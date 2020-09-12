using System;
using UnityEngine;
using UnityEngine.Serialization;
using MM26.IO.Models;

namespace MM26.IO
{
    public class WebSocketDataProvider : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        private SceneConfiguration _sceneConfiguration = null;

        [SerializeField]
        private Data _data = null;

        [SerializeField]
        private SceneLifeCycle _sceneLifeCycle = null;

        private WebSocketListener _listener = null;

        private void OnEnable()
        {
            _sceneLifeCycle.FetchData.AddListener(this.OnFetchData);
        }

        private void OnDisable()
        {
            _sceneLifeCycle.FetchData.RemoveListener(this.OnFetchData);
        }

        private void OnDestroy()
        {
            _listener.Dispose();
        }

        private void OnFetchData()
        {
            _listener = WebSocketListener.Platform;

            Action onConnection = () =>
            {
                // Please preserve this log message for diagnostic purpose
                Debug.Log("Connected");
            };

            Action onError = () =>
            {
                // Please preserve this log message for diagnostic purpose
                Debug.LogError("Connection Failed");
            };

            _listener.NewMessage += this.OnMessage;
            _listener.Connect(new Uri(_sceneConfiguration.WebSocketURL), onConnection, onError);
        }

        private void OnMessage(object sender, byte[] message)
        {
            if (_data.Initial == null)
            {
                _data.Initial = VisualizerInitial.Parser.ParseFrom(message);
                _sceneLifeCycle.DataFetched.Invoke();
            }
            else
            {
                _data.Turns.Enqueue(VisualizerTurn.Parser.ParseFrom(message));
            }

            // Please preserve this log message for diagnostic purpose
            Debug.LogFormat("Message received, length = {0}", message.Length);
        }
    }
}
