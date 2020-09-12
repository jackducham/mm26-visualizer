using Unity.Entities;

namespace MM26.Components
{
    /// <summary>
    /// Movement of entities
    /// </summary>
    [GenerateAuthoringComponent]
    public struct MovementSettings : IComponentData
    {
        /// <summary>
        /// At how close to the target do we stop moving
        /// </summary>
        public float Tolerance;

        /// <summary>
        /// Smooth time used in smooth damping
        /// </summary>
        public float Smoothtime;
    }
}