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
                .ForEach((Transform transform, ID id, Rotation rotation) =>
                {
                    if (!this.TasksToFinish.TryGetValue(id.Name, out Task task))
                    {
                        return;
                    }

                    RotationTask rotationTask = (RotationTask)task;

                    if (!rotationTask.IsStarted)
                    {
                        this.TasksToFinish[id.Name].Start();
                        rotation.Amount = rotationTask.RotationAmount;
                        rotation.Axis = rotationTask.RotationAxis;
                    }

                    if (rotation.Amount > 0.0f)
                    {
                        float rotAmt = rotation.Speed * Time.DeltaTime;
                        if (rotation.Amount < rotAmt)
                        {
                            rotAmt = rotation.Amount;
                            // Finish
                            this.Finish(this.TasksToFinish[id.Name]);
                        }

                        rotation.Amount -= rotAmt;

                        transform.Rotate(rotation.Axis, rotAmt);
                    }
                })
                .Run();
        }
    }

}
