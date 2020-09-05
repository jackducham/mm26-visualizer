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
    public class TasksManager : ScriptableObject
    {
        [SerializeField]
        private Mailbox _mailbox = null;

        private List<TasksBatch> _batches = null;

        private void OnEnable()
        {
            this.Reset();
        }

        public void Reset()
        {
            _batches = new List<TasksBatch>();
        }

        /// <summary>
        /// Queue a batch of tasks.
        /// </summary>
        /// <param name="tasks">Batch of tasks to run.</param>
        public void AddTasksBatch(TasksBatch tasks)
        {
            _batches.Add(tasks);
        }

        /// <summary>
        /// Updates the status of the current batches. Will handle changing between batches.
        /// </summary>
        public void Update()
        {
            if (_batches.Count > 0)
            {
                if (!_batches[0].IsStarted)
                {
                    _batches[0].Start(_mailbox);
                }

                _batches[0].Update();

                if (_batches[0].IsFinished)
                {
                    _batches.RemoveAt(0);
                }
            }
        }
    }
}
