using UnityEngine;
using UnityEngine.UI;
using Google.Protobuf;
using MM26.IO;
using MM26.IO.Models;

namespace MM26.Scenes.IO.ProtocolBuffer
{
    public class ProtocolBufferSceneController : MonoBehaviour
    {
        [SerializeField]
        private Text _turnNumberText = null;

        private byte[] _bytes = null;

        void Awake()
        {
            GameState state = new GameState
            {
                StateId = 17,
            };

            _bytes = state.ToByteArray();
        }

        void Start()
        {
            GameState state = GameState.Parser.ParseFrom(_bytes);
            _turnNumberText.text = $"Turn Number = {state.StateId}";
        }
    }
}
