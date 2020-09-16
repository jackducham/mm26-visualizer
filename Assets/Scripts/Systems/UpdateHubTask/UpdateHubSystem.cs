using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using MM26.Components;
using MM26.ECS;
using MM26.Tasks;

namespace MM26.Systems.UpdateHubTask
{
    public class UpdateHubSystem : SystemBase
    {
        private Mailbox _mailbox;
        private Dictionary<string, Tasks.UpdateHubTask> _tasks;

        protected override void OnCreate()
        {
            base.OnCreate();

            _mailbox = Resources.Load<Mailbox>("Objects/Mailbox");
            _mailbox.SubscribeToTaskType<Tasks.FollowPathTask>(this);

            _tasks = new Dictionary<string, Tasks.UpdateHubTask>();
        }

        protected override void OnUpdate()
        {
            Task[] tasks = _mailbox.GetSubscribedTasksForType<Tasks.UpdateHubTask>(this);

            for (int i = 0; i < tasks.Length; i++)
            {
                Tasks.UpdateHubTask updateHubTask = tasks[i] as Tasks.UpdateHubTask;
                _tasks[updateHubTask.EntityName] = updateHubTask;
                _mailbox.RemoveTask(updateHubTask);
            }

            this.Entities
                .WithoutBurst()
                .ForEach((Character character, Hub hub) =>
                {
                    if (_tasks.TryGetValue(character.name, out Tasks.UpdateHubTask task))
                    {
                        if (task.Health.HasValue)
                        {
                            hub.HealthLabel.text = task.Health.Value + "";
                        }
                    }
                })
                .Run();
        }
    }
}