using UnityEngine;
using UnityEngine.UI;
using Google.Protobuf;
using MM26.IO;

namespace MM26.Scenes.IO.ProtocolBuffer
{
    public class ProtocolBufferSceneController : MonoBehaviour
    {
        [SerializeField]
        private Text _turnNumberText = null;

        private byte[] _bytes = null;

        void Awake()
        {
            VisualizerTurn turn = new VisualizerTurn
            {
                TurnNumber = 17,
            };

            _bytes = turn.ToByteArray();
        }

        void Start()
        {
            VisualizerTurn turn = VisualizerTurn.Parser.ParseFrom(_bytes);
            _turnNumberText.text = $"Turn Number = {turn.TurnNumber}";
        }
    }
}
