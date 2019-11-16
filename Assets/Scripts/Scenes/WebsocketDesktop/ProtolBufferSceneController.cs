using UnityEngine;
using System.IO;
using Google.Protobuf;
using MM26.IO;

namespace MM26.Scenes.WebsocketDesktop
{
    public class ProtolBufferSceneController : MonoBehaviour
    {
        MemoryStream stream = new MemoryStream();

        void Awake()
        {
            VisualizerTurn turn = new VisualizerTurn
            {
                TurnNumber = 17,
            };

            turn.WriteTo(this.stream);

            byte[] buffer = this.stream.GetBuffer();
        }

        void Start()
        {
            VisualizerTurn turn = VisualizerTurn.Parser.ParseFrom(this.stream);

            Debug.Log(turn.TurnNumber);
        }
    }
}
