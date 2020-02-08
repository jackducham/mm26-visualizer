﻿using System;
using UnityEngine;

namespace MM26.IO
{
    internal class WebGLDataProvider : WebDataProvider
    {
        private class WebGLDataReceiver : MonoBehaviour
        {
            public event EventHandler<byte[]> NewData;

            public void ReceiveData(byte[] bytes)
            {
                this.NewData?.Invoke(this, bytes);
            }
        }

        GameObject _gameObject;

        internal WebGLDataProvider()
        {
            _gameObject = new GameObject("WebGLDataReceiver");
            WebGLDataReceiver receiver = _gameObject.AddComponent<WebGLDataReceiver>();

            receiver.NewData += (sender, data) =>
            {
                this.AddChange(data);
            };
        }
    }
}
