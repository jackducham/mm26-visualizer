using Unity.Entities;
using MM26.Components;

namespace MM26.Systems.FollowPathTask
{
    [UpdateInGroup(typeof(TaskCheckingSystemGroup))]
    public class FollowPathTaskCheckingSystem : SystemBase
    {
        EntityCommandBufferSystem _ecbSystem;
        FolowPathTaskTranslationSystem _translationSystem;

        protected override void OnCreate()
        {
            base.OnCreate();

            _ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            _translationSystem = World.GetOrCreateSystem<FolowPathTaskTranslationSystem>();
        }

        protected override void OnUpdate()
        {
            EntityCommandBuffer ecb = _ecbSystem.CreateCommandBuffer();

            this.Entities
                .WithoutBurst()
                .ForEach((Entity entity, Character character, FollowPath followPath) =>
                {
                    if (_translationSystem.RunningTasks.TryGetValue(character.name, out Tasks.FollowPathTask task))
                    {
                        if (followPath.Progress == task.Path.Length)
                        {
                            task.IsFinished = true;

                            ecb.RemoveComponent<FollowPath>(entity);
                            ecb.RemoveComponent<PathElement>(entity);

                            _translationSystem.RunningTasks.Remove(character.name);
                        }
                    }
                })
                .Run();
        }
    }
}