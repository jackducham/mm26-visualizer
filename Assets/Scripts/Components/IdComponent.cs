using UnityEngine;

namespace MM26.Components
{
    public class IdComponent : MonoBehaviour
    {
        private static int _nextId;
        public int id = _nextId++;
    }
}
