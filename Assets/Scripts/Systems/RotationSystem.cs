using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using MM26.Components;
using MM26.Tasks;
using MM26.ECS;

namespace MM26.Systems
{
    public class RotationSystem : TaskSystem<RotationTask>
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
                .ForEach((Transform transform, IDComponent id, RotationComponent rotation) =>
                {
                    Task task;

                    if (!this.TasksToFinish.TryGetValue(id.Name, out task))
                    {
                        return;
                    }

                    RotationTask rotationTask = (RotationTask)task;

                    if (!rotationTask.IsStarted)
                    {
                        this.TasksToFinish[id.Name].Start();
                        rotation.amount = rotationTask.RotationAmount;
                        rotation.axis = rotationTask.RotationAxis;
                    }

                    if (rotation.amount > 0.0f)
                    {
                        float rotAmt = rotation.Speed * Time.DeltaTime;
                        if (rotation.amount < rotAmt)
                        {
                            rotAmt = rotation.amount;
                            // Finish
                            this.TasksToFinish[id.Name].Finish();
                        }

                        rotation.amount -= rotAmt;

                        transform.Rotate(rotation.axis, rotAmt);
                    }
                })
                .Run();
        }
    }

}
