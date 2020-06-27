using UnityEngine;
using UnityEngine.Events;

namespace MM26
{
    [CreateAssetMenu(menuName = "Services/Data", fileName = "Data")]
    public class DataService : StatefulService
    {
        [SerializeField]
        UnityEvent _connected = null;

        public UnityEvent Connected => _connected;

        public void Connect()
        {
        }

        public override void Reset()
        {
            
        }
    }
}
