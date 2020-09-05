using UnityEngine;

namespace MM26.Components
{
    public class Movement : MonoBehaviour
    {
        public float Tolerance = 0.5f;
        public float Smoothtime = 0.5f;
        public int Progress = 0;

        public Vector3 CurrentVelocity;
    }
}