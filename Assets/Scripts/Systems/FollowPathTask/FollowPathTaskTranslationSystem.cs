using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using MM26.Components;
using MM26.ECS;

namespace MM26.Systems.FollowPathTask
{
    [UpdateInGroup(typeof(TaskTranslationSystemGroup))]
    public class FolowPathTaskTranslationSystem : SystemBase
    {
        public Dictionary<string, Tasks.FollowPathTask> RunningTasks { get; private set; }

        EntityCommandBufferSystem _ecbSystem;
        Mailbox _mailbox = null;
        Dictionary<string, Tasks.FollowPathTask> _tasksToBeScheduled;

        protected override void OnCreate()
        {
            base.OnCreate();

            _ecbSystem = World
                .GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();

            _mailbox = Resources.Load<Mailbox>("Objects/Mailbox");
            _mailbox.SubscribeToTaskType<Tasks.FollowPathTask>(this);

            _tasksToBeScheduled = new Dictionary<string, Tasks.FollowPathTask>();
            this.RunningTasks = new Dictionary<string, Tasks.FollowPathTask>();
        }

        protected override void OnUpdate()
        {
            Tasks.FollowPathTask[] tasks = _mailbox.GetSubscribedTasksForType<Tasks.FollowPathTask>(this);


            for (int i = 0; i < tasks.Length; i++)
            {
                Tasks.FollowPathTask movementTask = tasks[i];
                _tasksToBeScheduled[movementTask.EntityName] = movementTask;
                _mailbox.RemoveTask(movementTask);
            }

            if (_tasksToBeScheduled.Count > 0)
            {
                this.Dispatch();
            }

            _tasksToBeScheduled.Clear();
            _ecbSystem.AddJobHandleForProducer(this.Dependency);
        }

        private void Dispatch()
        {
            EntityCommandBuffer ecb = _ecbSystem.CreateCommandBuffer();

            this.Entities
                .WithoutBurst()
                .ForEach((Entity entity, Character character) =>
                {
                    if (_tasksToBeScheduled.TryGetValue(character.name, out Tasks.FollowPathTask task))
                    {
                        FollowPath followPath = new FollowPath()
                        {
                            Progress = 0,
                            CurrentVelocity = new Vector3()
                        };

                        ecb.AddComponent<FollowPath>(entity, followPath);

                        DynamicBuffer<PathElement> path = ecb.AddBuffer<PathElement>(entity);

                        for (int i = 0; i < task.Path.Length; i++)
                        {
                            path.Add(new PathElement()
                            {
                                Position = task.Path[i]
                            });
                        }

                        this.RunningTasks.Add(character.name, task);
                    }
                })
                .Run();
        }
    }
}
