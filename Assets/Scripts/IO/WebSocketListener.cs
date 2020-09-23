using System;
using System.IO;
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
        private readonly ClientWebSocket _client = new ClientWebSocket();
        private readonly byte[] _buffer = new byte[2048];
        private readonly MemoryStream _stream = new MemoryStream();

        public override void Dispose()
        {
            base.Dispose();
            _client.Dispose();
            _stream.Dispose();
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
                var buffer = new ArraySegment<byte>(_buffer);

                WebSocketReceiveResult result = await _client.ReceiveAsync(
                    buffer,
                    CancellationToken.None);

                _stream.Write(buffer.Array, buffer.Offset, result.Count);

                if (result.EndOfMessage)
                {
                    _stream.Seek(0, SeekOrigin.End);
                    var message = new byte[_stream.Seek(0, SeekOrigin.Current)];

                    _stream.Seek(0, SeekOrigin.Begin);
                    _stream.Read(message, 0, message.Length);
                    _stream.Seek(0, SeekOrigin.Begin);

                    this.NewMessage?.Invoke(this, message);
                }
            }
        }
    }
#endif
}
