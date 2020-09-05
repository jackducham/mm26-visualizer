using UnityEngine;
using Unity.Entities;
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

            this.Entities
                .WithoutBurst()
                .ForEach((ID id, Transform transform, Movement movement) =>
                {
                    if (this.TasksToFinish.TryGetValue(id.Name, out Task task))
                    {
                        MovementTask movementTask = (MovementTask)task;

                        Vector3 target = movementTask.Path[movement.Progress];

                        transform.position = Vector3.SmoothDamp(
                            current: transform.position,
                            target: target,
                            currentVelocity: ref movement.CurrentVelocity,
                            smoothTime: movement.Smoothtime);

                        if (Vector3.Distance(target, transform.position) <= movement.Tolerance)
                        {
                            transform.position = target;
                            movement.Progress++;
                        }

                        if (movement.Progress == movementTask.Path.Length)
                        {
                            this.Finish(movementTask);
                        }
                    }
                })
                .Run();
        }
    }
}
