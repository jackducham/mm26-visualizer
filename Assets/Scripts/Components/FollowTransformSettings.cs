using Unity.Entities;

namespace MM26.Components
{
    [GenerateAuthoringComponent]
    public struct FollowTransformSettings : IComponentData
    {
        public float SmoothTime;
    }
}
