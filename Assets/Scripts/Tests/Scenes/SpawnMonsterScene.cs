using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MM26.ECS;
using MM26.Tasks;

namespace MM26.Tests.Scenes
{
    public class SpawnMonsterScene : MonoBehaviour
    {
        [SerializeField]
        private Mailbox _mailbox = null;

        private void Start()
        {
            _mailbox.SendTask(new SpawnMonsterTask("monster", new Vector3Int(), "mm26_tiles/lizard.png"));
        }
    }
}
