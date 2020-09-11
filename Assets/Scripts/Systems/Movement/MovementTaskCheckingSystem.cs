using UnityEngine;
using Unity.Entities;
using MM26.Components;
using MM26.Tasks;

namespace MM26.Systems.Movement
{
    [UpdateInGroup(typeof(TaskCheckingSystemGroup))]
    public class MovementTaskCheckingSystem : SystemBase
    {
        EntityCommandBufferSystem _ecbSystem;
        MovementTaskTranslationSystem _translationSystem;

        protected override void OnCreate()
        {
            base.OnCreate();

            _ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            _translationSystem = World.GetOrCreateSystem<MovementTaskTranslationSystem>();
        }

        protected override void OnUpdate()
        {
            EntityCommandBuffer ecb = _ecbSystem.CreateCommandBuffer();

            this.Entities
                .WithoutBurst()
                .ForEach((Entity entity, Components.Movement movement, Moving moving) =>
                {
                    if (_translationSystem.RunningTasks.TryGetValue(movement.name, out MovementTask task))
                    {
                        if (movement.Progress == task.Path.Length)
                        {
                            task.IsFinished = true;
                            ecb.RemoveComponent<Moving>(entity);
                            _translationSystem.RunningTasks.Remove(movement.name);
                        }
                    }
                })
                .Run();
        }
    }
}