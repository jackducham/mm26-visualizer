using UnityEngine;
using UnityEngine.Events;

namespace MM26
{
    public class DataProvider : Resettable
    {
        [SerializeField]
        public Data _data = null;

        [Header("Events")]
        [SerializeField]
        UnityEvent _canBegin = null;

        public UnityEvent CanBegin => _canBegin;

        public void Start()
        {
        }

        public override void Reset()
        {
            
        }
    }
}
