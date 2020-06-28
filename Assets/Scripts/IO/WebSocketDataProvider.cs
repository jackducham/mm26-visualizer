﻿using System;
using UnityEngine;
using UnityEngine.Serialization;
using MM26.IO.Models;

namespace MM26.IO
{
    public class WebSocketDataProvider : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        private string _editorUri = "ws://localhost:5000/visualizer";

        [SerializeField]
#pragma warning disable 414
        private string _buildUri = "";
#pragma warning restore 414

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
            };

            Action onError = () =>
            {
                Debug.LogError("Failed");
            };

            _listener.NewMessage += this.OnMessage;

#if UNITY_EDITOR
            _listener.Connect(new Uri(_editorUri), onConnection, onError);
#else
            _listener.Connect(new Uri(_releaseUri), onConnection, onError);
#endif
        }

        private void OnMessage(object sender, byte[] message)
        {
            if (_data.GameState == null)
            {
                _data.GameState = GameState.Parser.ParseFrom(message);
                _sceneLifeCycle.DataFetched.Invoke();
            }
            else
            {
                _data.GameChanges.Enqueue(GameChange.Parser.ParseFrom(message));
            }
        }
    }
}
