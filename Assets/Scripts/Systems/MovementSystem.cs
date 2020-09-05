﻿using UnityEngine;
using Unity.Entities;
using MM26.ECS;
using MM26.Components;
using MM26.Tasks;

namespace MM26.Systems
{
    public class MovementSystem : TaskSystem<MovementTask>
    {
        protected override void OnCreate()
        {
            base.OnCreate();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            this.Entities
                .WithoutBurst()
                .ForEach((ID id, Transform transform, Movement movement) =>
                {
                    Task task;

                    if (this.TasksToFinish.TryGetValue(id.Name, out task))
                    {
                        MovementTask movementTask = (MovementTask)task;

                        task.Start();
                        task.Finish();

                        //transform.position = movementTask.Destination.position;
                    }
                })
                .Run();
        }
    }
}
