using UnityEngine;
using System.Collections.Generic;

namespace MM26.ECS
{
    /// <summary>
    /// Class <c>TasksManager</c>
    /// Manages the task batches.
    /// </summary>
    /// <remarks>Will manage adding batches, starting batches, and determine when to move to the next batch.</remarks>
    [CreateAssetMenu(fileName = "TaskManager", menuName = "ECS/Task Manager")]
    public sealed class TasksManager : ScriptableObject
    {
        [SerializeField]
        private Mailbox _mailbox = null;

        private Queue<TasksBatch> _batches = null;

        private void OnEnable()
        {
            this.Reset();
        }

        public void Reset()
        {
            _batches = new Queue<TasksBatch>();
        }

        /// <summary>
        /// Queue a batch of tasks.
        /// </summary>
        /// <param name="tasks">Batch of tasks to run.</param>
        public void AddTasksBatch(TasksBatch tasks)
        {
            _batches.Enqueue(tasks);
        }

        /// <summary>
        /// Updates the status of the current batches. Will handle changing between batches.
        /// </summary>
        public void Update()
        {
            if (_batches.Count > 0)
            {
                TasksBatch top = _batches.Peek();

                if (!top.IsStarted)
                {
                    top.Start(_mailbox);
                }

                top.Update();

                if (top.IsFinished)
                {
                    _batches.Dequeue();
                }
            }
        }
    }
}
