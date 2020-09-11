using UnityEngine;
using Unity.Entities;
using MM26.ECS;
using MM26.Components;
using MM26.Tasks;

namespace MM26.Systems.Movement
{
    /// <summary>
    /// The system that moves entities
    /// </summary>
    [UpdateInGroup(typeof(TaskExecutionSystemGroup))]
    public class MovementSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            this.Entities
                .WithoutBurst()
                .ForEach((ID id, Transform transform, Components.Movement movement, Moving moving) =>
                {
                    Vector3 target = movement.Path[movement.Progress];

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

                    
                })
                .Run();
        }
    }
}
