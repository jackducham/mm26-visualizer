using System;
using UnityEngine;
using MM26.IO;
using TMPro;

namespace MM26.Tests
{
    /// <summary>
    /// Used to control Connection test scene
    /// </summary>
    public sealed class ConnectionTestScene : MonoBehaviour
    {
        [SerializeField]
        private string _uri = "ws://localhost:8081/visualizer/";

        private WebSocketListener _websocketListener = null;

        private void OnDisable()
        {
            if (_websocketListener != null)
            {
                _websocketListener.Dispose();
            }
        }

        private void Awake()
        {
            _websocketListener = WebSocketListener.Platform;
            _websocketListener.NewMessage += this.OnNewMessage;

            try
            {
                var uri = new Uri(_uri);

                _websocketListener.Connect(
                    uri,
                    // success
                    () =>
                    {
                        Debug.Log("Connected");
                    },
                    // failure
                    () =>
                    {
                        _websocketListener.Dispose();
                        _websocketListener = null;
                        Debug.LogError("Failed to connect");
                    });
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        private void OnNewMessage(object sender, byte[] data)
        {
            Debug.LogFormat("New message, size = {0} bytes", data.Length);
        }
    }
}
