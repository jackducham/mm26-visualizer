using UnityEngine;
using UnityEngine.Events;
using Google.Protobuf;
using MM26.IO.Models;

namespace MM26.IO
{
    public class DataProvider : Resettable
    {
        [SerializeField]
        private Data _data = null;

        [Header("Events")]
        [SerializeField]
        UnityEvent _canStart = null;

        public Data Data => _data;
        public UnityEvent CanStart => _canStart;

        public virtual void Start()
        {
        }

        public override void Reset()
        {
            
        }
    }
}
