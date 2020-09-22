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

        private void Start()
        {
            _mailbox.SendTask(new SpawnPlayerTask("player", new Vector3Int()));
            StartCoroutine(this.Simulate());
        }

        private IEnumerator Simulate()
        {
            yield return new WaitForSecondsRealtime(5.0f);

            _mailbox.SendTask(new FollowPathTask("player", new Vector3[]
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
