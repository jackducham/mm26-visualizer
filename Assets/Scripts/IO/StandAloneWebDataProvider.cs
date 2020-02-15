using System;

namespace MM26.IO
{
#if UNITY_STANDALONE
    internal class StandAloneWebDataProvider : WebDataProvider
    {
        WebSocketListener _changeListener;

        internal StandAloneWebDataProvider() : base()
        {
            _changeListener = new WebSocketListener();
            _changeListener.NewMessage += (sender, bytes) =>
            {
                this.RunOnMainThread(() =>
                {
                    this.ProcessBytes(bytes);
                });
            };
        }

        public override void Dispose()
        {
            _changeListener.Dispose();
        }

        public override void UseEndpoints(NetworkEndpoints endpoints, Action onConnection, Action onFailure)
        {
            base.UseEndpoints(endpoints, onConnection, onFailure);
            _changeListener.Connect(new Uri(endpoints.ChangeSocket), onConnection, onFailure);
        }
    }
#endif
}