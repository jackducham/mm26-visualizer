using UnityEngine;
using MM26.ECS;
using MM26.Tasks;

namespace MM26.Board
{
    public class BoardUpdator : MonoBehaviour
    {
        [SerializeField]
        private CharactersManager _charactersManager = null;

        [SerializeField]
        private EffectsManager _effectsManager = null;

        [SerializeField]
        private Mailbox _mailbox = null;

        private void OnEnable()
        {
            _mailbox.SubscribeToTaskType<SpawnPlayerTask>(this);
            _mailbox.SubscribeToTaskType<DespawnTask>(this);
            _mailbox.SubscribeToTaskType<EffectTask>(this);
        }

        private void Update()
        {
            this.HandleSpawnTasks();
            this.HandleDespawnTasks();
            this.HandleEffectTasks();
        }

        /// <summary>
        /// Helper function to handle spawn tasks
        ///
        /// 
        /// </summary>
        private void HandleSpawnTasks()
        {
            SpawnPlayerTask[] tasks = _mailbox.GetSubscribedTasksForType<SpawnPlayerTask>(this);

            for (int i = 0; i < tasks.Length; i++)
            {
                SpawnPlayerTask task = tasks[i];

                _charactersManager.CreatePlayer(task.Position, task.EntityName);

                task.IsFinished = true;
                _mailbox.RemoveTask(task);
            }
        }

        /// <summary>
        /// Helper function to handle despawn tasks
        /// 
        /// <b>Noe that this is called once per frame</b>
        /// </summary>
        private void HandleDespawnTasks()
        {
            DespawnTask[] tasks = _mailbox.GetSubscribedTasksForType<DespawnTask>(this);

            for (int i = 0; i < tasks.Length; i++)
            {
                DespawnTask task = tasks[i];

                // FIXME: might cause performance issue (this is on a hot path)
                GameObject entity = GameObject.Find(task.EntityName);
                GameObject.Destroy(entity);

                task.IsFinished = true;
                _mailbox.RemoveTask(task);
            }
        }

        private void HandleEffectTasks()
        {
            EffectTask[] tasks = _mailbox.GetSubscribedTasksForType<EffectTask>(this);

            for (int i = 0; i < tasks.Length; i++)
            {
                EffectTask task = tasks[i];

                switch (task.Type)
                {
                    case EffectType.Death:
                        _effectsManager.CreateDeathEffect(task.Position);
                        break;
                    case EffectType.Portal:
                        _effectsManager.CreatePortalEffect(task.Position);
                        break;
                    case EffectType.Spawn:
                        _effectsManager.CreateSpawnEffect(task.Position);
                        break;
                }

                _mailbox.RemoveTask(task);
            }
        }
    }
}
