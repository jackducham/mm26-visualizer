using UnityEngine;

namespace MM26.ECS
{
    /// <summary>Class <c>Task</c>:
    /// Represents a task on a specific entity for systems to complete.
    /// </summary>
    /// <remarks>When extending this class, do not try to overload this class with too much functionality. Instead, think of it as a collection of data for a system to read.</remarks>
    ///
    public abstract class Task
    {
        public readonly string EntityName;

        private static int _nextId;
        private readonly int _taskId;

        /// <summary>
        /// Check whether a task has been marked as finished or not.
        /// </summary>
        /// <returns>True if the task is marked as finished; false otherwise.</returns>
        public bool IsFinished { get; set; }

        /// <summary>
        /// Check whether a task has been marked as started or not.
        /// </summary>
        /// <returns>True if the task is marked as started; false otherwise.</returns>
        public bool IsStarted { get; set; }

        public Task(string entityName)
        {
            this.EntityName = entityName;
            this.IsFinished = false;

            _taskId = _nextId++;
        }

        /// <summary>
        /// Mark the task as having started being used. Useful for classes to know whether a task has been touched yet or not.
        /// </summary>
        public void Start()
        {
            IsStarted = true;
        }

        /// <summary>
        /// Get the id of the task.
        /// </summary>
        /// <returns>id of the task.</returns>
        public int GetId()
        {
            return _taskId;
        }
    }
}
