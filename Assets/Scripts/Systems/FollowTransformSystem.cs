using UnityEngine;
using Unity.Entities;
using MM26.Components;

namespace MM26.Systems
{
    public class FollowTransformSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            this.Entities
                .WithoutBurst()
                .ForEach((
                    Transform transform,
                    FollowTransform followTransform,
                    in FollowTransformSettings settings) =>
                {
                    transform.position = Vector3.SmoothDamp(
                        transform.position,
                        followTransform.Target.position + followTransform.Offset,
                        ref followTransform.CurrentVelocity,
                        settings.SmoothTime);
                })
                .Run();
        }
    }
}
