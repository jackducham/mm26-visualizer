using System.Collections;
using UnityEngine;
using MM26.ECS;
using MM26.Tasks;

namespace MM26.Tests.Scenes
{
    public class DespawnScene : MonoBehaviour
    {
        [SerializeField]
        Mailbox _mailbox = null;

        // Start is called before the first frame update
        private void Start()
        {
            _mailbox.SendTask(new SpawnPlayerTask("player", new Vector3Int()));
            StartCoroutine(this.Destroy());
        }

        private IEnumerator Destroy()
        {
            yield return new WaitForSecondsRealtime(1.0f);
            _mailbox.SendTask(new DespawnTask("player"));
        }
    }
}
