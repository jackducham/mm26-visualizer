using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace MM26.ECS
{
    /// <summary>
    /// Base class for task systems
    /// </summary>
    /// <typeparam name="T">the task type</typeparam>
    public abstract class TaskSystem<T> : SystemBase where T : Task
    {
        /// <summary>
        /// A list of tasks to finish
        /// </summary>
        /// <typeparam name="string">the entity the task is associated with</typeparam>
        /// <typeparam name="Task">the task</typeparam>
        /// <returns></returns>
        protected Dictionary<string, Task> TasksToFinish { get; private set; }

        /// <summary>
        /// The readonly mailbox object
        /// </summary>
        /// <value></value>
        protected Mailbox Mailbox { get; private set; }

        /// <summary>
        /// Subclass should must this method to provide a custom mailbox
        /// </summary>
        /// <returns></returns>
        protected virtual Mailbox GetMailbox()
        {
            return Resources.Load<Mailbox>("Objects/Mailbox");
        }

        protected bool ShouldFinish()
        {
            return true;
        }

        protected override void OnCreate()
        {
            base.OnCreate();

            this.Mailbox = this.GetMailbox();
            this.TasksToFinish = new Dictionary<string, Task>();
        }

        /// <summary>
        /// Subclass must call TaskSystem's <c>OnUpdate</c> before doing
        /// anything else
        /// </summary>
        protected override void OnUpdate()
        {
            // Loop through all the entities with ids.
            // Check to see if that id has an associated task
            this.UpdateMessages();
        }

        private void UpdateMessages()
        {
            List<Task> messages = this.Mailbox.GetSubscribedTasksForType<T>(this);

            if (messages == null)
            {
                return;
            }

            foreach (var msg in messages)
            {
                if (!msg.IsFinished)
                {
                    this.TasksToFinish[msg.EntityName] = msg;
                    //msg.FinishMessage();
                }
            }
        }
    }
}
