using System.Collections;
using System.Collections.Generic;

namespace MM26.ECS
{
    /// <summary>
    /// Class <c>TasksBatch</c>:
    /// Represents a batch of tasks to be completed in parallel.
    /// </summary>
    /// <remarks>This is a non-ordered collection of data, so think of it more like a set than an array.</remarks>
    public sealed class TasksBatch : IEnumerable<Task>
    {
        /// <summary>
        /// List of tasks
        /// </summary>
        public List<Task> Tasks => _tasks;

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

        private List<Task> _tasks;

        public TasksBatch()
        {
            _tasks = new List<Task>();

            this.IsFinished = false;
            this.IsStarted = false;
        }

        /// <summary>
        /// Adds a task to the batch.
        /// </summary>
        /// <param name="task">Task to add to the batch.</param>
        public void Add(Task task)
        {
            _tasks.Add(task);
        }

        /// <summary>
        /// Helper function to directly set the batch of tasks insteading of adding tasks one by one.
        /// </summary>
        /// <param name="tasks">The batch of tasks to use.</param>
        public void SetTasksList(List<Task> tasks)
        {
            _tasks = tasks;
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

            for (int i = 0; i < _tasks.Count; i++)
            {
                if (!_tasks[i].IsFinished)
                {
                    IsFinished = false;
                    break;
                }
            }
        }

        /// <summary>
        /// Start the batch of tasks. Will send out the tasks to the Mailbox for systems to read from and operate on.
        /// </summary>
        public void Start(Mailbox mailbox)
        {
            for (int i = 0; i < _tasks.Count; i++)
            {
                mailbox.SendTask(_tasks[i]);
            }

            IsStarted = true;
        }

        public IEnumerator<Task> GetEnumerator()
        {
            return _tasks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _tasks.GetEnumerator();
        }
    }
}
