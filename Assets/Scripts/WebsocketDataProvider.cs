using UnityEngine;
using UnityEngine.Events;

namespace MM26
{
    [CreateAssetMenu(menuName = "Data/Web Socket Data Provider", fileName = "WebsocketDataProvider")]
    public class WebsocketDataProvider : DataProvider
    {
        [SerializeField]
        private UnityEvent _connected = null;

        public UnityEvent Connected => _connected;

        public void Connect()
        {
            
        }

        public override void Start()
        {
            Debug.Log("Start Websocket Data Provider");
            base.Start();
            this.CanStart.Invoke();
        }
    }
}
