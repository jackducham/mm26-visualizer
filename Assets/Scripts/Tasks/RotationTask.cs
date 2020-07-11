using UnityEngine;
using MM26.ECS;

namespace MM26.Tasks
{
    public class RotationTask : Task
    {
        public float rotationAmount;
        public Vector3 rotationAxis;

        public RotationTask(int id, Mailbox mailbox, float rotationAmt, Vector3 axis) : base(id, mailbox)
        {
            rotationAmount = rotationAmt;
            rotationAxis = axis;
        }

        public override string GetTaskType()
        {
            return "Rotation";
        }

        public static string Type => "Rotation";
    }
}
