using System;
using UnityEngine;
using UnityEngine.Events;
using MM26.IO.Models;

namespace MM26.IO
{
    [CreateAssetMenu(menuName = "Data/Web Socket Data Provider", fileName = "WebsocketDataProvider")]
    public class WebSocketDataProvider : DataProvider
    {
        [Header("Settings")]
        [SerializeField]
        private string _debugUri = "";

        [SerializeField]
        private string _releaseUri = "";

        [Header("Events")]
        [SerializeField]
        private UnityEvent _connected = null;

        [SerializeField]
        private UnityEvent _failed = null;

        private WebSocketListener _listener = null;

        public UnityEvent Connected => _connected;
        public UnityEvent Failed => _failed;

        public void Connect()
        {
            _listener = WebSocketListener.Platform;

            Action onConnection = () =>
            {
                this.Connected.Invoke();
            };

            Action onError = () =>
            {
                this.Failed.Invoke();
                Debug.LogError("Failed");
            };

            _listener.NewMessage += this.OnMessage;

#if UNITY_EDITOR
            _listener.Connect(new Uri(_debugUri), onConnection, onError);
#else
            _listener.Connect(new Uri(_releaseUri), onConnection, onError);
#endif
        }

        public override void Start()
        {
            base.Start();

            if (_listener == null)
            {
                this.Connect();
            }
        }

        public override void Reset()
        {
            base.Reset();

            _listener.Dispose();
            _listener = null;
        }

        private void OnMessage(object sender, byte[] message)
        {
            if (this.Data.GameState == null)
            {
                this.Data.GameState = GameState.Parser.ParseFrom(message);
                this.CanStart.Invoke();
            }
            else
            {
                this.Data.GameChanges.Enqueue(GameChange.Parser.ParseFrom(message));
            }
        }
    }
}
