using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace MM26.ECS
{
    /// <summary>
    /// The mailbox serves as a mediator between the Tasks and the actual
    /// systems. Tasks are logged in the <c>Mailbox</c> by the
    /// <c>TasksManager</c> and read off from here by systems.
    /// </summary>
    [CreateAssetMenu(fileName = "Mailbox", menuName = "ECS/Mailbox")]
    public class Mailbox : ScriptableObject
    {
        /// <summary>
        /// Map objects to their task types
        /// </summary>
        private Dictionary<object, List<string>> _subscriptionMapping = null;

        /// <summary>
        /// Map task types to tasks
        /// </summary>
        private Dictionary<string, List<Task>> _mailbox = null;
        private List<Task> _potentialTasksToDelete = null;

        private void OnEnable()
        {
            this.Reset();
        }

        /// <summary>
        /// Called to reset the mailbox at a scene's end
        /// </summary>
        public void Reset()
        {
            _subscriptionMapping = new Dictionary<object, List<string>>();
            _mailbox = new Dictionary<string, List<Task>>();
            _potentialTasksToDelete = new List<Task>();
        }

        /// <summary>
        /// Subscribe a subscriber object to a task type
        /// </summary>
        /// <param name="o">the subscriber object</param>
        /// <typeparam name="T">the task type</typeparam>
        public void SubscribeToTaskType<T>(object o) where T : Task
        {
            if (!_subscriptionMapping.TryGetValue(o, out List<string> taskTypes))
            {
                taskTypes = new List<string>();
                _subscriptionMapping[o] = taskTypes;
            }

            taskTypes.Add(typeof(T).Name);
        }

        public void SendTask(Task task)
        {
            string taskName = task.GetType().Name;

            if (!_mailbox.TryGetValue(taskName, out List<Task> tasks))
            {
                tasks = new List<Task>();
                _mailbox[taskName] = tasks;
            }

            tasks.Add(task);
        }

        /// <summary>
        /// Get tasks for a registered subscriber object
        /// </summary>
        /// <param name="o">the subscriber object</param>
        /// <typeparam name="T">the task type</typeparam>
        /// <returns></returns>
        public ReadOnlyCollection<Task> GetSubscribedTasksForType<T>(object o) where T : Task
        {
            List<Task> tasks = null;

            string msgType = typeof(T).Name;

            if (_subscriptionMapping.TryGetValue(o, out List<string> msgTypes))
            {
                if (msgTypes.Contains(msgType))
                {
                    _mailbox.TryGetValue(msgType, out tasks);
                }
            }

            if (tasks == null)
            {
                Debug.LogErrorFormat("The object has not subscribed to messages of {0}", msgType);
            }

            return tasks.AsReadOnly();
        }

        public void RemoveTask(Task msg)
        {
            string taskName = msg.GetType().Name;

            // Remove from mailbox
            _mailbox[taskName].RemoveAll(m => m != null && m.GetId() == msg.GetId());

            // Remove from potential mesages to delete
            _potentialTasksToDelete.RemoveAll(m => m != null && m.GetId() == msg.GetId());
        }

        public void Update()
        {
            var msgsToDelete = new List<Task>();

            for (int i = 0; i < _potentialTasksToDelete.Count; i++)
            {
                Task msg = _potentialTasksToDelete[i];

                if (msg != null && msg.IsFinished)
                {
                    msgsToDelete.Add(msg);
                }
            }

            for (int i = 0; i < msgsToDelete.Count; i++)
            {
                Task msg = msgsToDelete[i];
                RemoveTask(msg);
            }
        }

    }

}
