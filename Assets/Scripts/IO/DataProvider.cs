using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using Google.Protobuf;

namespace MM26.IO
{
    public sealed class DataProvider: MonoBehaviour
    {
        [Header("Events")]
        [SerializeField]
        UnityEvent _newChange;

        [Header("Settings")]
        [SerializeField]
        string _changeUrl;

#if UNITY_EDITOR
        [SerializeField]
        string _debugChangeUrl;
#endif
        MessageParser<VisualizerChange> _changeParser = null;
        MessageParser<VisualizerTurn> _turnParser = null;

        Dictionary<long, VisualizerChange> _changes;

        

#if UNITY_STANDALONE
        WebSocketListener _changeListener;
#endif
        long _latestChangeNumer = 0;

        public long LatestChangeNumber
        {
            get { return _latestChangeNumer; }
        }

        private void Awake()
        {
#if UNITY_STANDALONE
            _changeListener = new WebSocketListener(SynchronizationContext.Current);
            _changeListener.NewMessage += this.ProcessChangeData;

#if UNITY_EDITOR
            _changeListener.Connect(new Uri(_debugChangeUrl));
#else
            _changeListener.Connect(new Uri(_changeUrl));
#endif
#endif
            _changes = new Dictionary<long, VisualizerChange>();
            _changeParser = VisualizerChange.Parser;
            _turnParser = VisualizerTurn.Parser;
        }

        public void ProcessChangeData(object sender, byte[] bytes)
        {
            VisualizerChange change = _changeParser.ParseFrom(bytes);

            _latestChangeNumer = change.ChangeNumber;
            _newChange.Invoke();

            _changes[change.ChangeNumber] = change;
        }

        public VisualizerChange GetChange(int change)
        {
            return _changes[change];
        }

        public VisualizerTurn GetTurn(int turn)
        {
            return null;
        }
    }
}