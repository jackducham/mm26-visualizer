using System.Collections;
using UnityEngine;
using MM26.ECS;
using MM26.Tasks;

namespace MM26.Tests.Scenes
{
    /// <summary>
    /// Test scene for effects
    /// </summary>
    public class EffectsScene : MonoBehaviour
    {
        [SerializeField]
        Mailbox _mailbox = null;

        private void Start()
        {
            StartCoroutine(this.Simulate());
        }

        private IEnumerator Simulate()
        {
            yield return new WaitForSecondsRealtime(1.0f);

            _mailbox.SendTask(new EffectTask(EffectType.Death, new Vector3Int(0, 0, 0)));
            yield return new WaitForSecondsRealtime(1.0f);

            _mailbox.SendTask(new EffectTask(EffectType.Spawn, new Vector3Int(0, 1, 0)));
            yield return new WaitForSecondsRealtime(1.0f);

            _mailbox.SendTask(new EffectTask(EffectType.Portal, new Vector3Int(1, 1, 0)));
            yield return new WaitForSecondsRealtime(1.0f);

            _mailbox.SendTask(new EffectTask(EffectType.Attack, new Vector3Int(2, 1, 0)));
        }
    }
}
