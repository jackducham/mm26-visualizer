using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Google.Protobuf;

namespace MM26.IO
{
    internal sealed class DataProvider
    {
        MessageParser<VisualizerChange> _changeParser = null;
        MessageParser<VisualizerTurn> _turnParser = null;

        Dictionary<long, VisualizerChange> _changes = new Dictionary<long, VisualizerChange>();

#if UNITY_STANDALONE
        WebSocketListener _changeListener;
#endif
        long _latestChangeNumer = 0;

        public EventHandler NewChange;

        public long LatestChangeNumber
        {
            get { return _latestChangeNumer; }
        }

        internal DataProvider(string changeSocket)
        {
#if UNITY_STANDALONE 
            _changeListener = new WebSocketListener(SynchronizationContext.Current);
            _changeListener.NewMessage += this.OnChangeData;
            _changeListener.Connect(new Uri(changeSocket));
#endif

            _changeParser = VisualizerChange.Parser;
            _turnParser = VisualizerTurn.Parser;
        }

        /// <summary>
        /// Process new change data in bytes
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="bytes">bytes representing the change</param>
        public void OnChangeData(object sender, byte[] bytes)
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
        public VisualizerChange GetChange(int change)
        {
            return _changes[change];
        }

        /// <summary>
        /// Get a turn
        /// </summary>
        /// <param name="turn">the turn number</param>
        /// <returns>a turn if there is one, null otherwise</returns>
        public VisualizerTurn GetTurn(int turn)
        {
            return null;
        }
    }
}