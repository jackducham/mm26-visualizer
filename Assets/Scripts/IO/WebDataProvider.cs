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
    internal class WebDataProvider : DataProvider, IWebDataProvider
    {
        public NetworkEndpoints Endpoints { get; set; }
        public event EventHandler<VisualizerChange> NewChange;

        public virtual void UseEndpoints(NetworkEndpoints endpoints, Action onConnection, Action onFailure)
        {
            this.Endpoints = endpoints;
        }

        public void AddChange(byte[] bytes)
        {
            VisualizerChange change = this.ChangeParser.ParseFrom(bytes);

            this.LatestChangeNumber = change.ChangeNumber;
            this.NewChange?.Invoke(this, change);

            this.Changes[change.ChangeNumber] = change;
        }

        public override void GetChange(long change, Action<VisualizerChange> callback)
        {
            this.Run(() =>
            {
                UnityWebRequest request = UnityWebRequest.Get("");

                while (!request.isDone)
                {
                }
            });

        }

        /// <summary>
        /// Run an action on main thread when in WebGL, and in a
        /// System.Threading.Task otherwise
        /// </summary>
        /// <param name="action"></param>
        protected void Run(Action action)
        {
#if UNITY_WEBGL
            action();
#endif
            Task.Run(action);
        }
    }
}
