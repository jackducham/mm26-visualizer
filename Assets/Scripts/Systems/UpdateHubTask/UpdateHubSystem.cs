﻿using System.Collections.Generic;
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
            _mailbox.SubscribeToTaskType<Tasks.UpdateHubTask>(this);

            _tasks = new Dictionary<string, Tasks.UpdateHubTask>();
        }

        protected override void OnUpdate()
        {
            Tasks.UpdateHubTask[] tasks = _mailbox.GetSubscribedTasksForType<Tasks.UpdateHubTask>(this);

            for (int i = 0; i < tasks.Length; i++)
            {
                Tasks.UpdateHubTask updateHubTask = tasks[i];
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
                            hub.Health = task.Health.Value;
                        }

                        if (task.Level.HasValue)
                        {
                            hub.Level = task.Level.Value;
                        }

                        if (task.Experience.HasValue)
                        {
                            hub.Experience = task.Experience.Value;
                        }

                        task.IsFinished = true;
                    }
                })
                .Run();
        }
    }
}