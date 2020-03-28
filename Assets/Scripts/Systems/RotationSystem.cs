using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using MM26.Components;
using MM26.Tasks;
using MM26.ECS;

public class RotationSystem : ComponentSystem
{
    EntityQuery _query = null;

    Dictionary<int, RotationTask> tasksToFinish;

    protected override void OnCreate()
    {
        base.OnCreate();

        tasksToFinish = new Dictionary<int, RotationTask>();
        _query = GetEntityQuery(typeof(Transform),
                                typeof(RotationComponent),
                                typeof(IdComponent));
        Mailbox.Instance.SubscribeToTaskType(this, RotationTask.TYPE_STRING);
    }

    private void UpdateMessages()
    {
        List<Task> messages = Mailbox.Instance.GetSubscribedTasksForType(this, RotationTask.TYPE_STRING);

        if (messages == null)
        {
            return;
        }
        foreach (RotationTask msg in messages)
        {
            if (!msg.IsFinished)
            {
                int id = msg.EntityID;
                tasksToFinish[id] = msg;
                //msg.FinishMessage();
            }
        }
    }

    protected override void OnUpdate()
    {
        UpdateMessages();

        var rotationComponents = _query.ToComponentArray<RotationComponent>();
        var transformComponents = _query.ToComponentArray<Transform>();
        var idComponents = _query.ToComponentArray<IdComponent>();

        for (int i = 0; i < transformComponents.Length; i++)
        {
            var rotationComponent = rotationComponents[i];
            var idComponent = idComponents[i];

            if (tasksToFinish.ContainsKey(idComponent.ID) && !tasksToFinish[idComponent.ID].IsStarted)
            {
                tasksToFinish[idComponent.ID].Start();
                rotationComponent.amount = tasksToFinish[idComponent.ID].rotationAmount;
                rotationComponent.axis = tasksToFinish[idComponent.ID].rotationAxis;
            }

            if (rotationComponent.amount > 0.0f)
            {
                var transformComponent = transformComponents[i];

                float rotAmt = rotationComponent.Speed * Time.deltaTime;
                if (rotationComponent.amount < rotAmt)
                {
                    rotAmt = rotationComponent.amount;
                    // Finish
                    tasksToFinish[idComponent.ID].Finish();
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
