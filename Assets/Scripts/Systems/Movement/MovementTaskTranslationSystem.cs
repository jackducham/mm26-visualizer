using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using MM26.Components;
using MM26.ECS;
using MM26.Tasks;

namespace MM26.Systems.Movement
{
    [UpdateInGroup(typeof(TaskConversionSystemGroup))]
    public class MovementTaskTranslationSystem : SystemBase
    {
        public Dictionary<string, MovementTask> RunningTasks { get; private set; }

        EntityCommandBufferSystem _ecbSystem;
        Mailbox _mailbox = null;
        Dictionary<string, MovementTask> _tasksToBeScheduled;

        protected override void OnCreate()
        {
            base.OnCreate();

            _ecbSystem = World
                .GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();

            _mailbox = Resources.Load<Mailbox>("Objects/Mailbox");
            _mailbox.SubscribeToTaskType<MovementTask>(this);

            _tasksToBeScheduled = new Dictionary<string, MovementTask>();
            this.RunningTasks = new Dictionary<string, MovementTask>();
        }

        protected override void OnUpdate()
        {
            ReadOnlyCollection<Task> tasks = _mailbox.GetSubscribedTasksForType<MovementTask>(this);

            for (int i = 0; i < tasks.Count; i++)
            {
                MovementTask movementTask = tasks[i] as MovementTask;
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
            EntityCommandBuffer buffer = _ecbSystem.CreateCommandBuffer();

            this.Entities
                .WithoutBurst()
                .ForEach((Entity entity, ID id, Components.Movement movement) =>
                {
                    if (_tasksToBeScheduled.TryGetValue(id.name, out MovementTask task))
                    {
                        movement.Progress = 0;
                        movement.Path = task.Path;

                        buffer.AddComponent<Moving>(entity);
                        this.RunningTasks.Add(id.Name, task);
                    }
                })
                .Run();
        }
    }
}
