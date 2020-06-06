using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace MM26.IO
{
#if UNITY_STANDALONE
    internal sealed class WebSocketListener: IDisposable
    {
        /// <summary>
        /// Called when a new message has been received. May run on a
        /// separate thread!
        /// </summary>
        internal event EventHandler<byte[]> NewMessage;

        bool _idle = true;
        ClientWebSocket _client = new ClientWebSocket();
        Buffer _buffer = new Buffer();

        public void Dispose()
        {
            _client.Dispose();
        }

        /// <summary>
        /// Connect to an uri
        /// </summary>
        /// <param name="uri">the uri to connect websocket to</param>
        internal void Connect(Uri uri)
        {
            this.Connect(uri, () => { }, () => { });
        }

        /// <summary>
        /// Connect to an uri
        /// </summary>
        /// <param name="uri">the uri to connect websocket to</param>
        /// <param name="onConnection">called after connection</param>
        /// <param name="onFailure">called if failed</param>
        internal void Connect(Uri uri, Action onConnection, Action onFailure)
        {
            if (_idle)
            {
                _client.ConnectAsync(uri, CancellationToken.None)
                    .ContinueWith(async (task) =>
                    {
                        if (_client.State != WebSocketState.Open)
                        {
                            _idle = true;
                            onFailure();

                            return;
                        }

                        //onConnection();
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
