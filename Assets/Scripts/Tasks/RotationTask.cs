using UnityEngine;
using MM26.ECS;

namespace MM26.Tasks
{
    public class RotationTask : Task
    {
        public float RotationAmount;
        public Vector3 RotationAxis;

        public RotationTask(
            string entityName,
            Mailbox mailbox,
            float rotationAmt,
            Vector3 axis) : base(entityName, mailbox)
        {
            RotationAmount = rotationAmt;
            RotationAxis = axis;
        }

        public override string GetTaskType()
        {
            return "Rotation";
        }

        public static string Type => "Rotation";
    }
}
