using System;

namespace MM26.IO
{
#if UNITY_STANDALONE
    /// <summary>
    /// Web data provider
    /// </summary>
    internal class StandAloneWebDataProvider : WebDataProvider
    {
        /// <summary>
        /// Listener for websocket
        /// </summary>
        private WebSocketListener _changeListener;

        /// <summary>
        /// Create an empty instance of the stand alone web data provider
        /// </summary>
        internal StandAloneWebDataProvider() : base()
        {
            _changeListener = new WebSocketListener();
            _changeListener.NewMessage += this.OnNewMessage;
        }

        private void OnNewMessage(object sender, byte[] bytes)
        {
            // Since WebSocketListener.NewMessage may run another thread,
            // we need to make sure that this is run on the main thread
            this.RunOnMainThread(() =>
            {
                this.ProcessBytes(bytes);
            });
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
