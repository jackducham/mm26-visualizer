using UnityEngine;
using Unity.Entities;

namespace MM26.Components
{
    public struct FollowPath : IComponentData
    {
        public int Progress;
        public Vector3 CurrentVelocity;
    }
}
