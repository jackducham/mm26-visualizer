using System.Collections.Generic;

namespace MM26.ECS
{
    /// <summary>
    /// Class <c>TasksBatch</c>:
    /// Represents a batch of tasks to be completed in parallel.
    /// </summary>
    /// <remarks>This is a non-ordered collection of data, so think of it more like a set than an array.</remarks>
    public class TasksBatch
    {

        private List<Task> _tasksSet;

        /// <summary>
        /// Check whether a batch has finished or not.
        /// </summary>
        /// <returns>True if the batch has finished; false otherwise.</returns>
        public bool IsFinished { get; set; }

        /// <summary>
        /// Check whether a batch has started or not.
        /// </summary>
        /// <returns>True if the batch has started; false otherwise.</returns>
        public bool IsStarted { get; set; }

        public TasksBatch()
        {
            _tasksSet = new List<Task>();
            IsFinished = false;
            IsStarted = false;
        }

        /// <summary>
        /// Adds a task to the batch.
        /// </summary>
        /// <param name="task">Task to add to the batch.</param>
        public void AddTask(Task task)
        {
            _tasksSet.Add(task);
        }

        /// <summary>
        /// Helper function to directly set the batch of tasks insteading of adding tasks one by one.
        /// </summary>
        /// <param name="tasks">The batch of tasks to use.</param>
        public void SetTasksList(List<Task> tasks)
        {
            _tasksSet = tasks;
        }

        /// <summary>
        /// Updates the finish status of the batch.
        /// </summary>
        public void Update()
        {
            if (IsFinished)
            {
                return;
            }

            IsFinished = true;

            for (int i = 0; i < _tasksSet.Count; i++)
            {
                if (!_tasksSet[i].IsFinished)
                {
                    IsFinished = false;
                }
            }
        }

        /// <summary>
        /// Start the batch of tasks. Will send out the tasks to the Mailbox for systems to read from and operate on.
        /// </summary>
        public void Start()
        {
            for (int i = 0; i < _tasksSet.Count; i++)
            {
                Mailbox.Instance.SendTask(_tasksSet[i]);

            }
            IsStarted = true;
        }

    }
}