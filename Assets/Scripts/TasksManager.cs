using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>Class <c>Task</c>:
/// Represents a task on a specific entity for systems to complete.
/// </summary>
/// <remarks>When extending this class, do not try to overload this class with too much functionality. Instead, think of it as a collection of data for a system to read.</remarks>
///
public abstract class Task {
    
    private static int _nextId;
    public int entityId;
    
    private readonly int _taskId;
    
	private bool _finished;
    private bool _started;
    
	public Task(int id) {
		entityId = id;
        _finished = false;
        _taskId = _nextId++;
	}
    
    /// <summary>
    /// Mark the task as having started being used. Useful for classes to know whether a task has been touched yet or not.
    /// </summary>
    public void Start() {
        _started = true;
    }

    /// <summary>
    /// Mark a task as finished. Useful for classes to know whether a task has been finished by a system.
    /// </summary>
    /// <remarks>Will remove the task from the Mailbox system so it will no longer be returned when getting messages for a type.</remarks>
    public void Finish() {
        _finished = true;
        Mailbox.Instance.RemoveTask(this);
    }

    /// <summary>
    /// The string representation of the task type. This is required for Mailbox to properly register the task.
    /// </summary>
    /// <remarks>Make sure that this task type is unique. If you have multiple task subclasses with the same task string, errors will like occur.</remarks>
    /// <returns>The string represention of the task type.</returns>
    public abstract string GetTaskType();
    
    /// <summary>
    /// Get the id of the task.
    /// </summary>
    /// <returns>id of the task.</returns>
    public int GetId() {
        return _taskId;
    }
    
    /// <summary>
    /// Check whether a task has been marked as finished or not.
    /// </summary>
    /// <returns>True if the task is marked as finished; false otherwise.</returns>
	public bool IsFinished() {
		return _finished;
	}
    
    /// <summary>
    /// Check whether a task has been marked as started or not.
    /// </summary>
    /// <returns>True if the task is marked as started; false otherwise.</returns>
    public bool IsStarted() {
        return _started;
    }
    
    
}

/// <summary>
/// Class <c>TasksBatch</c>:
/// Represents a batch of tasks to be completed in parallel.
/// </summary>
/// <remarks>This is a non-ordered collection of data, so think of it more like a set than an array.</remarks>
public class TasksBatch {
    
	private IList<Task> _tasksSet;
    private bool _finished;
    private bool _started;
    
    public TasksBatch() {
        _tasksSet = new List<Task>();
        _finished = false;
        _started = false;
    }

    /// <summary>
    /// Check whether a batch has finished or not.
    /// </summary>
    /// <returns>True if the batch has finished; false otherwise.</returns>
    public bool IsFinished() {
        return _finished;
    }

    /// <summary>
    /// Check whether a batch has started or not.
    /// </summary>
    /// <returns>True if the batch has started; false otherwise.</returns>
    public bool IsStarted() {
        return _started;
    }
    
    /// <summary>
    /// Adds a task to the batch.
    /// </summary>
    /// <param name="task">Task to add to the batch.</param>
    public void AddTask(Task task) {
        _tasksSet.Add(task);
    }
    
    /// <summary>
    /// Helper function to directly set the batch of tasks insteading of adding tasks one by one.
    /// </summary>
    /// <param name="tasks">The batch of tasks to use.</param>
    public void SetTasksList(IList<Task> tasks) {
        _tasksSet = tasks;
    }
    
    /// <summary>
    /// Updates the finish status of the batch.
    /// </summary>
    public void Update() {
        if (_finished) {
            return;
        }
        
        _finished = true;
        for (int i = 0; i < _tasksSet.Count; i++) {
            if (!_tasksSet[i].IsFinished()) {
                _finished = false;
            }
        }
    }
    
    /// <summary>
    /// Start the batch of tasks. Will send out the tasks to the Mailbox for systems to read from and operate on.
    /// </summary>
    public void Start() {
        for (int i = 0; i < _tasksSet.Count; i++) {
            Mailbox.Instance.SendTask(_tasksSet[i]);
            
        }
        _started = true;
    }
    
}

/// <summary>
/// Class <c>TasksManager</c>
/// Manages the task batches.
/// </summary>
/// <remarks>Will manage adding batches, starting batches, and determine when to move to the next batch.</remarks>
public class TasksManager : MonoBehaviour
{
	private IList<TasksBatch> _batches;
    
    public TasksManager() {
        _batches = new List<TasksBatch>();
    }
    
    /// <summary>
    /// Queue a batch of tasks.
    /// </summary>
    /// <param name="tasks">Batch of tasks to run.</param>
	public void AddTasksBatch(TasksBatch tasks) {
		_batches.Add(tasks);
	}
    
    /// <summary>
    /// Updates the status of the current batches. Will handle changing between batches.
    /// </summary>
	public void Update() {
		//Mailbox.Instance.Update();
        
        if (_batches.Count > 0) {
            if (!_batches[0].IsStarted()) {
                _batches[0].Start();
            }
            _batches[0].Update();
            
            if (_batches[0].IsFinished()) {
                _batches.RemoveAt(0);
            }
        }
	}
    
}