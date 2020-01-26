using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace MM26.IO
{
    public sealed class WebSocketListener: IDisposable
    {
        public event EventHandler<byte[]> NewMessage;

        bool _idle = false;
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
            if (!_idle)
            {
                _client.ConnectAsync(uri, CancellationToken.None)
                    .ContinueWith(async (task) =>
                    {
                        callback();
                        await this.OnConnect();
                    });

                _idle = false;
            }
        }

        async Task OnConnect()
        {
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
}
