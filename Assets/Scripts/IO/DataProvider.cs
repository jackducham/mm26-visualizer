using System.Threading;
using UnityEngine;
using Google.Protobuf;

namespace MM26.IO
{
    public sealed class DataProvider: MonoBehaviour
    {
        private MessageParser<VisualizerChange> _changeParser = null;
        private MessageParser<VisualizerTurn> _turnParser = null;

#if UNITY_STANDALONE
        WebSocketListener _webSocketListener;
#endif

        private void Awake()
        {
#if UNITY_STANDALONE
            _webSocketListener = new WebSocketListener(SynchronizationContext.Current);
            _webSocketListener.NewMessage += this.ProcessChangeData;
#endif
            _changeParser = VisualizerChange.Parser;
            _turnParser = VisualizerTurn.Parser;
        }

        public void ProcessChangeData(object sender, byte[] bytes)
        {
            Debug.Log("Got new bytes");
        }

        public VisualizerChange GetChange(int fromTurn, int toTurn)
        {
            return null;
        }

        public VisualizerTurn GetTurn(int turn)
        {
            return null;
        }
    }
}