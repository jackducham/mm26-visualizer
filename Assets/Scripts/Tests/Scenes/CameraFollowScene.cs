using System.Collections;
using UnityEngine;
using MM26.ECS;
using MM26.Tasks;

namespace MM26.Tests.Scenes
{
    public class CameraFollowScene : MonoBehaviour
    {
        [SerializeField]
        Mailbox _mailbox = null;

        [SerializeField]
        IO.Data _data = null;

        [SerializeField]
        SceneLifeCycle _sceneLifeCycle = null;

        private void Start()
        {
            var state = new IO.Models.GameState();
            state.BoardNames["pvp"] = new IO.Models.Board()
            {
                Height = 1,
                Width = 1
            };

            state.BoardNames["pvp"].Grid.Add(new IO.Models.Tile());

            _data.Initial = new IO.Models.VisualizerInitial()
            {
                State = state
            };

            _sceneLifeCycle.DataFetched.Invoke();


            _mailbox.SendTask(new SpawnPlayerTask("Player 1", new Vector3Int()));
            StartCoroutine(this.Simulate());
        }

        private IEnumerator Simulate()
        {
            yield return new WaitForSecondsRealtime(5.0f);

            _mailbox.SendTask(new FollowPathTask("Player 1", new Vector3[]
            {
                new Vector3(0.0f, 1.0f),
                new Vector3(0.0f, 2.0f),
                new Vector3(0.0f, 3.0f),
                new Vector3(0.0f, 4.0f),
                new Vector3(0.0f, 5.0f),
                new Vector3(0.0f, 6.0f),
                new Vector3(0.0f, 7.0f),
                new Vector3(0.0f, 8.0f),
            }));
        }
    }
}
