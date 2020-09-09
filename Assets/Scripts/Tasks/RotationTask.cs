using UnityEngine;
using MM26.ECS;

namespace MM26.Tasks
{
    /// <summary>
    /// Currently not used
    /// </summary>
    public sealed class RotationTask : Task
    {
        public float RotationAmount;
        public Vector3 RotationAxis;

        public RotationTask(
            string entityName,
            float rotationAmt,
            Vector3 axis) : base(entityName)
        {
            RotationAmount = rotationAmt;
            RotationAxis = axis;
        }
    }
}
