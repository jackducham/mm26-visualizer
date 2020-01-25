using UnityEngine;
using Google.Protobuf;

namespace MM26.IO
{
    public class DataProvider: MonoBehaviour
    {
#region Variables
        private MessageParser<VisualizerChange> _changeParser = null;
        private MessageParser<VisualizerTurn> _turnParser = null;
#endregion

#region MonoBehaviour APIs
        private void Awake()
        {
            _changeParser = VisualizerChange.Parser;
            _turnParser = VisualizerTurn.Parser;
        }
#endregion

#region JS to C Sharp Functions
        private void ProcessChangeData(byte[] bytes)
        {
            Debug.Log("Got new bytes");
        }
#endregion

#region Public APIs
        public VisualizerChange GetChange(int fromTurn, int toTurn)
        {
            return null;
        }

        public VisualizerTurn GetTurn(int turn)
        {
            return null;
        }
#endregion
    }
}