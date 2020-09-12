using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using MM26.Components;
using MM26.ECS;

namespace MM26.Systems.MovementTask
{
    [UpdateInGroup(typeof(TaskConversionSystemGroup))]
    public class MovementTaskTranslationSystem : SystemBase
    {
        public Dictionary<string, Tasks.MovementTask> RunningTasks { get; private set; }

        EntityCommandBufferSystem _ecbSystem;
        Mailbox _mailbox = null;
        Dictionary<string, Tasks.MovementTask> _tasksToBeScheduled;

        protected override void OnCreate()
        {
            base.OnCreate();

            _ecbSystem = World
                .GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();

            _mailbox = Resources.Load<Mailbox>("Objects/Mailbox");
            _mailbox.SubscribeToTaskType<Tasks.MovementTask>(this);

            _tasksToBeScheduled = new Dictionary<string, Tasks.MovementTask>();
            this.RunningTasks = new Dictionary<string, Tasks.MovementTask>();
        }

        protected override void OnUpdate()
        {
            ReadOnlyCollection<Task> tasks = _mailbox.GetSubscribedTasksForType<Tasks.MovementTask>(this);


            for (int i = 0; i < tasks.Count; i++)
            {
                Tasks.MovementTask movementTask = tasks[i] as Tasks.MovementTask;
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
                    if (_tasksToBeScheduled.TryGetValue(character.name, out Tasks.MovementTask task))
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
