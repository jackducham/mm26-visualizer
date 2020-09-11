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
                .ForEach((Entity entity, ID id, Components.Movement movement, Moving moving) =>
                {
                    if (_translationSystem.RunningTasks.TryGetValue(id.Name, out MovementTask task))
                    {
                        if (movement.Progress == task.Path.Length)
                        {
                            task.IsFinished = true;
                            ecb.RemoveComponent<Moving>(entity);
                            _translationSystem.RunningTasks.Remove(id.Name);
                        }
                    }
                })
                .Run();
        }
    }
}