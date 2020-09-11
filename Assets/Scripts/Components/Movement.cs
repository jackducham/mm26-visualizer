using UnityEngine;

namespace MM26.Components
{
    /// <summary>
    /// Movement of entities
    /// </summary>
    public class Movement : MonoBehaviour
    {
        /// <summary>
        /// At how close to the target do we stop moving
        /// </summary>
        public float Tolerance = 0.5f;

        /// <summary>
        /// Smooth time used in smooth damping
        /// </summary>
        public float Smoothtime = 0.5f;

        /// <summary>
        /// Toward which position are we moving
        /// </summary>
        public int Progress = 0;

        /// <summary>
        /// Used by smooth damping algorithm
        /// </summary>
        public Vector3 CurrentVelocity;

        [HideInInspector]
        public Vector3[] Path = null;
    }
}