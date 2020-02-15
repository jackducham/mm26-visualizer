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
    internal class WebDataProvider : DataProvider, IWebDataProvider
    {
        public NetworkEndpoints Endpoints { get; set; }
        public long StartTurnNumber { get; set; } = 0;
        public event EventHandler<GameChange> NewChange;

#if !UNITY_WEBGL
        protected SynchronizationContext SynchronizationContext;
#endif

        private bool _hasStartTurn = false;
        
        public WebDataProvider()
        {
            SynchronizationContext = SynchronizationContext.Current;
        }


        public virtual void UseEndpoints(NetworkEndpoints endpoints, Action onConnection, Action onFailure)
        {
            this.Endpoints = endpoints;
        }

        protected void ProcessBytes(byte[] bytes)
        {
            if (!_hasStartTurn)
            {
                GameState turn = this.TurnParser.ParseFrom(bytes);
                this.Turns[turn.TurnNumber] = turn;

                _hasStartTurn = true;
                StartTurnNumber = turn.TurnNumber;

                return;
            }

            GameChange change = this.ChangeParser.ParseFrom(bytes);

            this.LatestChangeNumber = change.TurnNumber;
            this.NewChange?.Invoke(this, change);

            this.Changes[change.TurnNumber] = change;
        }

        public override void GetChange(long change, Action<GameChange> callback)
        {
            this.Run(() =>
            {
                UnityWebRequest request = UnityWebRequest.Post(this.Endpoints.ChangeHttp, this.Endpoints.ChangeHttp);
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
