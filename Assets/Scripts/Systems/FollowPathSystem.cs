using UnityEngine;
using Unity.Entities;
using MM26.Components;

namespace MM26.Systems.MovementTask
{
    /// <summary>
    /// The system that moves entities
    /// </summary>
    [UpdateInGroup(typeof(TaskExecutionSystemGroup))]
    public class FollowPathSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            this.Entities
                .WithoutBurst()
                .ForEach((
                    Transform transform,
                    ref FollowPath followPath,
                    in MovementSettings movementSettings,
                    in DynamicBuffer<PathElement> path) =>
                {
                    Vector3 target = path[followPath.Progress].Position;

                    transform.position = Vector3.SmoothDamp(
                        current: transform.position,
                        target: target,
                        currentVelocity: ref followPath.CurrentVelocity,
                        smoothTime: movementSettings.Smoothtime);

                    if (Vector3.Distance(target, transform.position) <= movementSettings.Tolerance)
                    {
                        transform.position = target;
                        followPath.Progress++;
                    }

                })
                .Run();
        }
    }
}
