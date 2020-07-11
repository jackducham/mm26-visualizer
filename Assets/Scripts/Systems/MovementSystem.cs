using UnityEngine;
using MM26.ECS;
using MM26.Components;
using MM26.Tasks;

namespace MM26.Systems
{
    public class MovementSystem : TaskSystem<MovementTask>
    {
        protected override void OnCreate()
        {
            base.OnCreate();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            this.Entities.ForEach((IdComponent id, Transform transform, MovementComponent movement) =>
            {
                MovementTask task = this.TasksToFinish[id.ID] as MovementTask;
                task.Start();
                task.Finish();

                transform.position = task.Destination.position;

                Debug.Log(this.TasksToFinish.Count);
            });
        }

        protected override Mailbox GetMailbox()
        {
            return Resources.Load<Mailbox>("Objects/Mailbox");
        }
    }
}
