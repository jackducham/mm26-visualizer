using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace MM26.IO.Assets.Scripts.IO
{
    public class WebSocketListener: IDisposable
    {
        public event EventHandler<byte[]> NewMessage;

        ClientWebSocket _client = new ClientWebSocket();
        
        int _count = 0;
        int _size = 1024;

        byte[] _buffer = null;

        public WebSocketListener()
        {
            _buffer = new byte[_size];
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
            _client.ConnectAsync(uri, CancellationToken.None)
                .ContinueWith(async (task) =>
                {
                    callback();
                    await this.OnConnect();
                });
        }

        async Task OnConnect()
        {
            while (_client.State == WebSocketState.Open)
            {
                // TODO: Implement buffer resizing
                ArraySegment<byte> buffer = new ArraySegment<byte>(_buffer, _count, _size);

                WebSocketReceiveResult result = await _client.ReceiveAsync(buffer, CancellationToken.None);

                if (result.EndOfMessage)
                {
                    this.NewMessage?.Invoke(this, _buffer);
                    continue;
                }

                _count += result.Count;
            }
        }
    }
}
