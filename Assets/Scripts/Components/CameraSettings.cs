using UnityEngine;
using Unity.Entities;

namespace MM26.Components
{
    [GenerateAuthoringComponent]
    public class CameraSettings : IComponentData
    {
        public Vector3 MoveSpeed;
        public float ZoomSpeed;
    }
}
