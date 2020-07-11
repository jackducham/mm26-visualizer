using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using MM26.ECS;
using MM26.Components;
using MM26.Tasks;

namespace MM26.Systems
{
    public class MovementSystem : ComponentSystem
    {
        Dictionary<int, Task> tasksToFinish = new Dictionary<int, Task>();
        private Mailbox _mailbox = null;

        protected override void OnCreate()
        {
            _mailbox = Resources.Load<Mailbox>("Objects/Mailbox");
            _mailbox.SubscribeToTaskType(this, MovementTask.Type);
        }

        protected override void OnUpdate()
        {
            this.UpdateMessages();

            this.Entities.ForEach((IdComponent id, Transform transform, MovementComponent movement) =>
            {
                MovementTask task = tasksToFinish[id.ID] as MovementTask;
                task.Start();
                task.Finish();

                transform.position = task.Destination.position;
            });
        }

        private void UpdateMessages()
        {
            List<Task> messages = _mailbox.GetSubscribedTasksForType(this, MovementTask.Type);

            if (messages == null)
            {
                return;
            }
            foreach (MovementTask msg in messages)
            {
                if (!msg.IsFinished)
                {
                    int id = msg.EntityID;
                    tasksToFinish[id] = msg;
                    //msg.FinishMessage();
                }
            }
        }
    }
}
