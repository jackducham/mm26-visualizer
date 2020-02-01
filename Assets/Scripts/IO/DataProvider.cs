using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Google.Protobuf;

namespace MM26.IO
{
    internal sealed class DataProvider: IDisposable
    {
        MessageParser<VisualizerChange> _changeParser = null;
        MessageParser<VisualizerTurn> _turnParser = null;

        Dictionary<long, VisualizerChange> _changes = new Dictionary<long, VisualizerChange>();

#if UNITY_STANDALONE
        WebSocketListener _changeListener;
#endif
        long _latestChangeNumer = 0;

        internal EventHandler NewChange;

        /// <summary>
        /// Change number of the newest change
        /// </summary>
        internal long LatestChangeNumber
        {
            get { return _latestChangeNumer; }
        }

        /// <summary>
        /// Initialize a new data provider
        /// </summary>
        internal DataProvider()
        {
#if UNITY_STANDALONE 
            _changeListener = new WebSocketListener(SynchronizationContext.Current);
            _changeListener.NewMessage += this.OnChangeData;
#endif

            _changeParser = VisualizerChange.Parser;
            _turnParser = VisualizerTurn.Parser;
        }

        public void Dispose()
        {
#if UNITY_STANDALONE
            _changeListener.Dispose();
#endif
        }

#if UNITY_STANDALONE
        /// <summary>
        /// Connect to a remote websocket server
        /// </summary>
        /// <param name="changeSocket">the address of the socket</param>
        /// <param name="onConnection">Called after connection</param>
        /// <param name="onFailure">Called if failed</param>
        internal void Connect(string changeSocket, Action onConnection, Action onFailure)
        {
            _changeListener.Connect(new Uri(changeSocket), onConnection, onFailure);
        }
#endif

        /// <summary>
        /// Process new change data in bytes
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="bytes">bytes representing the change</param>
        internal void OnChangeData(object sender, byte[] bytes)
        {
            VisualizerChange change = _changeParser.ParseFrom(bytes);

            _latestChangeNumer = change.ChangeNumber;
            NewChange?.Invoke(this, new EventArgs());

            _changes[change.ChangeNumber] = change;
        }

        /// <summary>
        /// Get a change
        /// </summary>
        /// <param name="change">the change number</param>
        /// <returns>a change if there is one, null otherwise</returns>
        internal VisualizerChange GetChange(int change)
        {
            return _changes[change];
        }

        /// <summary>
        /// Get a turn
        /// </summary>
        /// <param name="turn">the turn number</param>
        /// <returns>a turn if there is one, null otherwise</returns>
        internal VisualizerTurn GetTurn(int turn)
        {
            return null;
        }
    }
}