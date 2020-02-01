using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace MM26.IO
{
#if UNITY_STANDALONE
    public sealed class WebSocketListener: IDisposable
    {
        public sealed class ConnectionEventArgs : EventArgs
        {
            public ClientWebSocket ClientWebSocket { get; set; }

            public ConnectionEventArgs(ClientWebSocket clientWebSocket)
            {
                this.ClientWebSocket = clientWebSocket;
            }
        }

        public event EventHandler<byte[]> NewMessage;
        public event EventHandler<ConnectionEventArgs> ConnectionFailed;


        bool _idle = true;
        ClientWebSocket _client = new ClientWebSocket();
        Buffer _buffer = new Buffer();

        SynchronizationContext _context;

        public WebSocketListener(SynchronizationContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public void Connect(Uri uri)
        {
            this.Connect(uri, () => { });
        }

        public void Connect(Uri uri, Action callback)
        {
            if (_idle)
            {
                _client.ConnectAsync(uri, CancellationToken.None)
                    .ContinueWith(async (task) =>
                    {
                        if (_client.State != WebSocketState.Open)
                        {
                            _idle = true;
                            this.ConnectionFailed?.Invoke(this, new ConnectionEventArgs(_client));

                            return;
                        }

                        callback();
                        await this.OnConnect();
                    });

                _idle = false;
            }
        }

        async Task OnConnect()
        {
            Debug.LogFormat("connection state = {0}", _client.State);

            while (_client.State == WebSocketState.Open)
            {
                ArraySegment<byte> segment = _buffer.ArraySegment;

                WebSocketReceiveResult result = await _client.ReceiveAsync(segment, CancellationToken.None);
                _buffer.Append(result.Count);

                if (result.EndOfMessage)
                {
                    if (_context != null)
                    {
                        _context.Post((state) =>
                        {
                            this.NewMessage?.Invoke(this, _buffer.Content);
                        }, null);
                    }
                    else
                    {
                        this.NewMessage?.Invoke(this, _buffer.Content);
                    }
                    
                    _buffer.Reset();
                }
            }
        }
    }
#endif
}
