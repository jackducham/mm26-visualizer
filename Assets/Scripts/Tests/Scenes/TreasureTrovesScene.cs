using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MM26.ECS;
using MM26.Tasks;

namespace MM26.Tests.Scenes
{
    public class TreasureTrovesScene : MonoBehaviour
    {
        [SerializeField]
        Mailbox _mailbox = null;

        private void Start()
        {
            StartCoroutine(this.Simulate());
        }

        private IEnumerator Simulate()
        {
            _mailbox.SendTask(new UpdateTileItemTask(new Vector2Int(0, 0), true));

            yield return new WaitForSecondsRealtime(1.0f);
            _mailbox.SendTask(new UpdateTileItemTask(new Vector2Int(0, 0), true));

            yield return new WaitForSecondsRealtime(1.0f);
            _mailbox.SendTask(new UpdateTileItemTask(new Vector2Int(0, 0), false));

            yield return new WaitForSecondsRealtime(1.0f);
            _mailbox.SendTask(new UpdateTileItemTask(new Vector2Int(0, 0), true));
        }
    }
}
