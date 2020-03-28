using UnityEngine;
using Unity.Entities;
using System;
using System.Collections.Generic;

class HybridBoxRotationSystem : ComponentSystem
{
    EntityQuery _query = null;

    IDictionary<int, RotationTask> tasksToFinish;

    protected override void OnCreate()
    {
        base.OnCreate();

        tasksToFinish = new Dictionary<int, RotationTask>();
        _query = GetEntityQuery(typeof(Transform),
                                typeof(HybridBoxRotationComponent),
                                typeof(IdComponent));
        Mailbox.Instance.SubscribeToTaskType(this, RotationTask.TYPE_STRING);
    }

    private void UpdateMessages()
    {
        IList<Task> messages = Mailbox.Instance.GetSubscribedTasksForType(this, RotationTask.TYPE_STRING);

        if (messages == null)
        {
            return;
        }
        foreach (RotationTask msg in messages)
        {
            if (!msg.IsFinished())
            {
                int id = msg.entityId;
                tasksToFinish[id] = msg;
                //msg.FinishMessage();
            }
        }
    }

    protected override void OnUpdate()
    {
        UpdateMessages();

        var rotationComponents = _query.ToComponentArray<HybridBoxRotationComponent>();
        var transformComponents = _query.ToComponentArray<Transform>();
        var idComponents = _query.ToComponentArray<IdComponent>();

        for (int i = 0; i < transformComponents.Length; i++)
        {
            var rotationComponent = rotationComponents[i];
            var idComponent = idComponents[i];

            if (tasksToFinish.ContainsKey(idComponent.id) && !tasksToFinish[idComponent.id].IsStarted())
            {
                tasksToFinish[idComponent.id].Start();
                rotationComponent.amount = tasksToFinish[idComponent.id].rotationAmount;
                rotationComponent.axis = tasksToFinish[idComponent.id].rotationAxis;
            }

            if (rotationComponent.amount > 0.0f)
            {
                var transformComponent = transformComponents[i];

                float rotAmt = rotationComponent.Speed * Time.deltaTime;
                if (rotationComponent.amount < rotAmt)
                {
                    rotAmt = rotationComponent.amount;
                    // Finish
                    tasksToFinish[idComponent.id].Finish();
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
