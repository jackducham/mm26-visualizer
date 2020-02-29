using System;
using UnityEngine;
using UnityEngine.Networking;
using MM26.IO.Models;

#if !UNITY_WEBGL
using System.Threading;
using System.Threading.Tasks;
#endif

namespace MM26.IO
{
    /// <summary>
    /// Base class of all web-based data providers
    /// </summary>
    internal class WebDataProvider : DataProvider, IWebDataProvider
    {
        /// <summary>
        /// Endpoints used by the data provider
        /// </summary>
        /// <value>endpoints</value>
        public NetworkEndpoints Endpoints { get; set; }

        /// <summary>
        /// The ID of the first state
        /// </summary>
        /// <value></value>
        public long FirstStateNumber { get; set; } = 0;

        /// <summary>
        /// Fired when a change has been received
        /// </summary>
        public event EventHandler<GameChange> NewChange;

#if !UNITY_WEBGL
        /// <summary>
        /// Sync context used to sync between threads
        /// </summary>
        protected SynchronizationContext SynchronizationContext;
#endif

        /// <summary>
        /// Whether the start state has been read
        /// </summary>
        private bool _hasFirstState = false;

        public WebDataProvider()
        {
            SynchronizationContext = SynchronizationContext.Current;
        }

        /// <summary>
        /// Use the endpoints for the data provider
        /// </summary>
        /// <param name="endpoints">the end points</param>
        /// <param name="onConnection">
        /// called when connection is established
        /// </param>
        /// <param name="onFailure">called when the connection failed</param>
        public virtual void UseEndpoints(NetworkEndpoints endpoints, Action onConnection, Action onFailure)
        {
            this.Endpoints = endpoints;
        }

        /// <summary>
        /// Handle bytes
        /// </summary>
        /// <param name="bytes">bytes</param>
        protected void ProcessBytes(byte[] bytes)
        {
            if (!_hasFirstState)
            {
                GameState turn = this.StateParser.ParseFrom(bytes);
                this.States[turn.StateNumber] = turn;

                _hasFirstState = true;
                FirstStateNumber = turn.StateNumber;

                return;
            }

            GameChange change = this.ChangeParser.ParseFrom(bytes);

            this.LatestChangeNumber = change.ChangeNumber;
            this.NewChange?.Invoke(this, change);

            this.Changes[change.ChangeNumber] = change;
        }

        /// <summary>
        /// Get a change that has been received
        /// </summary>
        /// <param name="change">the change id</param>
        /// <param name="callback">fired when the change is available</param>
        public override void GetChange(long change, Action<GameChange> callback)
        {
            this.Run(() =>
            {
                UnityWebRequest request = UnityWebRequest.Post(
                    this.Endpoints.ChangeHttp,
                    this.Endpoints.ChangeHttp);

                UnityWebRequestAsyncOperation sendRequestOperation = request.SendWebRequest();

                sendRequestOperation.completed += (AsyncOperation operation) =>
                {
                    if (request.isDone)
                    {
                        DownloadHandler handler = request.downloadHandler;

                        if (handler.data != null)
                        {
                            GameChange newChange = this.ChangeParser.ParseFrom(handler.data);

                            this.RunOnMainThread(() =>
                            {
                                callback(newChange);
                            });
                        }

                        request.Dispose();
                    }
                };
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

        /// <summary>
        /// Either schedule or execute an action on the main thread
        /// </summary>
        /// <param name="action">the action</param>
        protected void RunOnMainThread(Action action)
        {
#if UNITY_WEBGL
            action();
#else
            if (this.SynchronizationContext != null)
            {
                this.SynchronizationContext.Post((state) =>
                {
                    action();
                }, null);
            }
#endif
        }
    }
}
