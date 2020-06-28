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
        UnityEvent _canStart = null;

        public UnityEvent CanStart => _canStart;

        public virtual void Start()
        {
        }

        public override void Reset()
        {
            
        }
    }
}
