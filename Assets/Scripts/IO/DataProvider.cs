using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Google.Protobuf;

namespace MM26.IO
{
    internal interface IDataProvider: IDisposable
    {
        /// <summary>
        /// Get a change
        /// </summary>
        /// <param name="change">the change number</param>
        /// <returns>a change if there is one, null otherwise</returns>
        VisualizerChange GetChange(long change);

        /// <summary>
        /// Get a turn
        /// </summary>
        /// <param name="turn">the turn number</param>
        /// <returns>a turn if there is one, null otherwise</returns>
        VisualizerTurn GetTurn(long change);
    }

    internal interface IWebDataProvider: IDataProvider
    {
        long LatestChangeNumber { get; }
        event EventHandler<VisualizerChange> NewChange;

        /// <summary>
        /// Connect to a remote websocket server
        /// </summary>
        /// <param name="changeSocket">the address of the socket</param>
        /// <param name="onConnection">Called after connection</param>
        /// <param name="onFailure">Called if failed</param>
        void Connect(Uri changeSocket, Action onConnection, Action onFailure);

        /// <summary>
        /// Process new change data in bytes
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="bytes">bytes representing the change</param>
        void AddChange(byte[] bytes);
    }

    internal class DataProvider: IDataProvider
    {
        public long LatestChangeNumber { get; protected set; }
        protected Dictionary<long, VisualizerChange> Changes { get; set; } = new Dictionary<long, VisualizerChange>();
        protected Dictionary<long, VisualizerTurn> Turns { get; set; } = new Dictionary<long, VisualizerTurn>();

        protected MessageParser<VisualizerChange> ChangeParser { get; set; }
        protected MessageParser<VisualizerTurn> TurnParser { get; set; }

        internal static IWebDataProvider CreateWebDataProvider()
        {
#if UNITY_STANDALONE
            return new StandAloneWebDataProvider();
#elif UNITY_WEBGL
            return new WebGLDataProvider();
#endif
        }

        internal static IDataProvider CreateFileSystemDataProvider()
        {
            return new StandAloneFileSystemDataProvider();
        }

        internal DataProvider()
        {
            this.ChangeParser = VisualizerChange.Parser;
            this.TurnParser = VisualizerTurn.Parser;
        }

        public virtual void Dispose() { }

        public virtual VisualizerChange GetChange(long change)
        {
            return this.Changes[change];
        }

        public virtual VisualizerTurn GetTurn(long change)
        {
            return this.Turns[change];
        }
    }

    internal class WebDataProvider: DataProvider, IWebDataProvider
    {
        public event EventHandler<VisualizerChange> NewChange;
        
        public virtual void Connect(Uri changeSocket, Action onConnection, Action onFailure)
        {
        }

        public void AddChange(byte[] bytes)
        {
            VisualizerChange change = this.ChangeParser.ParseFrom(bytes);

            this.LatestChangeNumber = change.ChangeNumber;
            this.NewChange?.Invoke(this, change);

            this.Changes[change.ChangeNumber] = change;
        }
    }


#if UNITY_STANDALONE
    internal class StandAloneWebDataProvider: WebDataProvider
    {
        WebSocketListener _changeListener;

        internal StandAloneWebDataProvider(): base()
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

        public override void Connect(Uri changeSocket, Action onConnection, Action onFailure)
        {
            _changeListener.Connect(changeSocket, onConnection, onFailure);
        }
    }

    internal class StandAloneFileSystemDataProvider : DataProvider
    {

    }
#endif

    internal class WebGLDataProvider: WebDataProvider
    {
        private class WebGLDataReceiver: MonoBehaviour
        {
            public event EventHandler<byte[]> NewData;

            public void ReceiveData(byte[] bytes)
            {
                this.NewData?.Invoke(this, bytes);
            }
        }

        GameObject _gameObject;

        internal WebGLDataProvider()
        {
            _gameObject = new GameObject("WebGLDataReceiver");
            WebGLDataReceiver receiver = _gameObject.AddComponent<WebGLDataReceiver>();

            receiver.NewData += (sender, data) =>
            {
                this.AddChange(data);
            };
        }
    }
}