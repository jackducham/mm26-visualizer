﻿using System;
using System.Collections;
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
        private float _waitProgress = 0.0f;

        private void OnEnable()
        {
            _sceneLifeCycle.FetchData.AddListener(this.TryConnect);
        }

        private void OnDisable()
        {
            _sceneLifeCycle.FetchData.RemoveListener(this.TryConnect);
        }

        private void OnDestroy()
        {
            if (_listener != null)
            {
                _listener.Dispose();
            }
        }

        private void Update()
        {
            if (_listener == null)
            {
                _waitProgress += Time.deltaTime;

                if (_waitProgress >= 5.0f)
                {
                    _waitProgress = 0.0f;
                    this.TryConnect();
                }
            }
        }

        private void TryConnect()
        {
            Debug.Log("Attempting to setup connection");

            _listener = WebSocketListener.Platform;

            Debug.LogFormat("thread id = {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);

            _listener.NewMessage += this.OnMessage;
            _listener.Connect(new Uri(_sceneConfiguration.WebSocketURL), this.OnConnection, this.OnError);
        }

        private void OnConnection()
        {
            // Please preserve this log message for diagnostic purpose
            Debug.Log("Connected");
        }

        private void OnError()
        {
            // Please preserve this log message for diagnostic purpose
            Debug.LogError("Connection Failed, reconnect in 5 seconds");
            _listener.Dispose();
            _listener = null;
        }

        private void OnMessage(object sender, byte[] message)
        {
            if (_data.Initial == null)
            {
                _data.Initial = VisualizerInitial.Parser.ParseFrom(message);

                Debug.Log(_data.Initial.State.BoardNames.Keys.Count);

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
