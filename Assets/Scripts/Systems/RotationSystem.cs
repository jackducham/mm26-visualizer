using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using MM26.Components;
using MM26.Tasks;
using MM26.ECS;

namespace MM26.Systems
{
    public class RotationSystem : ComponentSystem
    {
        EntityQuery _query = default;
        Dictionary<string, RotationTask> tasksToFinish;
        private Mailbox _mailbox = null;

        protected override void OnCreate()
        {
            base.OnCreate();

            tasksToFinish = new Dictionary<string, RotationTask>();
            _query = GetEntityQuery(
                typeof(Transform),
                typeof(RotationComponent),
                typeof(IDComponent));
            _mailbox = Resources.Load<Mailbox>("Objects/Mailbox");
            _mailbox.SubscribeToTaskType<RotationTask>(this);
        }

        private void UpdateMessages()
        {
            List<Task> messages = _mailbox.GetSubscribedTasksForType<RotationTask>(this);

            if (messages == null)
            {
                return;
            }
            foreach (RotationTask msg in messages)
            {
                if (!msg.IsFinished)
                {
                    tasksToFinish[msg.EntityName] = msg;
                    //msg.FinishMessage();
                }
            }
        }

        protected override void OnUpdate()
        {
            UpdateMessages();

            var rotationComponents = _query.ToComponentArray<RotationComponent>();
            var transformComponents = _query.ToComponentArray<Transform>();
            var idComponents = _query.ToComponentArray<IDComponent>();

            for (int i = 0; i < transformComponents.Length; i++)
            {
                var rotationComponent = rotationComponents[i];
                var idComponent = idComponents[i];

                if (tasksToFinish.ContainsKey(idComponent.Name) && !tasksToFinish[idComponent.Name].IsStarted)
                {
                    tasksToFinish[idComponent.Name].Start();
                    rotationComponent.amount = tasksToFinish[idComponent.Name].rotationAmount;
                    rotationComponent.axis = tasksToFinish[idComponent.Name].rotationAxis;
                }

                if (rotationComponent.amount > 0.0f)
                {
                    var transformComponent = transformComponents[i];

                    float rotAmt = rotationComponent.Speed * Time.DeltaTime;
                    if (rotationComponent.amount < rotAmt)
                    {
                        rotAmt = rotationComponent.amount;
                        // Finish
                        tasksToFinish[idComponent.Name].Finish();
                    }
                    rotationComponent.amount -= rotAmt;

                    transformComponent.Rotate(rotationComponent.axis, rotAmt);
                }
            }
            // Entities.ForEach((Transform transform, BoxRotationComponent component) =>
            // {
            //     transform.Translate(Vector3.forward * Time.deltaTime * component.Speed);
            // });
        }
    }

}
