using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Google.Protobuf;

#if !UNITY_WEBGL
using System.Threading;
using System.Threading.Tasks;
#endif

namespace MM26.IO
{
#if UNITY_STANDALONE
    internal class StandAloneWebDataProvider : WebDataProvider
    {
        WebSocketListener _changeListener;

        internal StandAloneWebDataProvider() : base()
        {
            _changeListener = new WebSocketListener(SynchronizationContext.Current);
            _changeListener.NewMessage += (sender, bytes) =>
            {
                this.AddChange(bytes);
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