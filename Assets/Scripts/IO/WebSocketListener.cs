using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace MM26.IO
{
    public abstract class WebSocketListener : IDisposable
    {
        /// <summary>
        /// Called when a new message has been received. May run on a
        /// separate thread!
        /// </summary>
        public EventHandler<byte[]> NewMessage;
        public abstract void Connect(Uri uri, Action onConnection, Action onError);

        public virtual void Dispose()
        {
        }

        public static WebSocketListener Platform
        {
            get
            {
                return new StandaloneWebSocketListener();
            }
        }
    }

#if UNITY_STANDALONE
    public sealed class StandaloneWebSocketListener : WebSocketListener
    {
        bool _idle = true;
        ClientWebSocket _client = new ClientWebSocket();
        Buffer _buffer = new Buffer();

        public override void Dispose()
        {
            base.Dispose();
            _client.Dispose();
        }

        /// <summary>
        /// Connect to an uri
        /// </summary>
        /// <param name="uri">the uri to connect websocket to</param>
        public void Connect(Uri uri)
        {
            this.Connect(uri, () => { }, () => { });
        }

        /// <summary>
        /// Connect to an uri
        /// </summary>
        /// <param name="uri">the uri to connect websocket to</param>
        /// <param name="onConnection">called after connection</param>
        /// <param name="onFailure">called if failed</param>
        public override void Connect(Uri uri, Action onConnection, Action onFailure)
        {
            if (_idle)
            {
                _client.ConnectAsync(uri, CancellationToken.None)
                    .ContinueWith(async (task) =>
                    {
                        if (_client.State != WebSocketState.Open)
                        {
                            _idle = true;
                            Debug.Log(_client.CloseStatus);
                            onFailure();

                            return;
                        }

                        onConnection();
                        await this.OnConnect();
                    });

                _idle = false;
            }
        }

        /// <summary>
        /// Handle connection
        /// </summary>
        /// <returns>some task</returns>
        async Task OnConnect()
        {
            while (_client.State == WebSocketState.Open)
            {
                ArraySegment<byte> segment = _buffer.ArraySegment;

                WebSocketReceiveResult result = await _client.ReceiveAsync(segment, CancellationToken.None);
                _buffer.Append(result.Count);

                if (result.EndOfMessage)
                {
                    byte[] bufferContent = _buffer.Content;
                    _buffer.Reset();

                    this.NewMessage?.Invoke(this, bufferContent);
                }
            }
        }
    }
#endif
}
