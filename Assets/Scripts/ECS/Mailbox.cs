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
    public sealed class Mailbox : ScriptableObject
    {
        /// <summary>
        /// Map objects to their task types
        /// </summary>
        private Dictionary<object, HashSet<string>> _subscriptionMapping = null;

        /// <summary>
        /// Tasks grouped by their identifiers
        /// </summary>
        private Dictionary<string, List<Task>> _tasks = null;

        private void OnEnable()
        {
            this.Reset();
        }

        /// <summary>
        /// Called to reset the mailbox at a scene's end
        /// </summary>
        public void Reset()
        {
            _subscriptionMapping = new Dictionary<object, HashSet<string>>();
            _tasks = new Dictionary<string, List<Task>>();
        }

        /// <summary>
        /// Subscribe a subscriber object to a task type
        /// </summary>
        /// <param name="o">the subscriber object</param>
        /// <typeparam name="T">the task type</typeparam>
        public void SubscribeToTaskType<T>(object o) where T : Task
        {
            string taskType = typeof(T).Name;

            if (!_subscriptionMapping.TryGetValue(o, out HashSet<string> taskTypes))
            {
                taskTypes = new HashSet<string>();
                _subscriptionMapping[o] = taskTypes;
            }

            taskTypes.Add(taskType);

            if (!_tasks.ContainsKey(taskType))
            {
                _tasks[taskType] = new List<Task>();
            }
        }

        public void SendTask(Task task)
        {
            string taskName = task.GetType().Name;

            if (!_tasks.TryGetValue(taskName, out List<Task> tasks))
            {
                tasks = new List<Task>();
                _tasks[taskName] = tasks;
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

            if (_subscriptionMapping.TryGetValue(o, out HashSet<string> msgTypes))
            {
                if (msgTypes.Contains(msgType))
                {
                    _tasks.TryGetValue(msgType, out tasks);
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
            _tasks[taskName].RemoveAll(m => m != null && m.GetId() == msg.GetId());
        }

        public void Update()
        {
        }
    }
}
