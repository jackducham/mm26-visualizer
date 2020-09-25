using UnityEngine;
using MM26.ECS;
using MM26.Tasks;
using MM26.Configuration;

namespace MM26.Board
{
    public class BoardUpdator : MonoBehaviour
    {
        [SerializeField]
        private CharactersManager _charactersManager = null;

        [SerializeField]
        private EffectsManager _effectsManager = null;

        [SerializeField]
        private TreasureTrovesManager _treasureTroveManager = null;

        [SerializeField]
        SpriteLookUp _spriteLookUp = null;

        [SerializeField]
        private Mailbox _mailbox = null;

        private void OnEnable()
        {
            _mailbox.SubscribeToTaskType<SpawnPlayerTask>(this);
            _mailbox.SubscribeToTaskType<SpawnMonsterTask>(this);
            _mailbox.SubscribeToTaskType<DespawnTask>(this);
            _mailbox.SubscribeToTaskType<EffectTask>(this);
            _mailbox.SubscribeToTaskType<UpdateTileItemTask>(this);
        }

        private void Update()
        {
            this.HandleSpawnPlayerTasks();
            this.HandleSpawnMonsterTasks();
            this.HandleDespawnTasks();
            this.HandleEffectTasks();
            this.HandleUpdateTileItemTasks();
        }

        /// <summary>
        /// Helper function to handle spawn tasks
        ///
        /// 
        /// </summary>
        private void HandleSpawnPlayerTasks()
        {
            SpawnPlayerTask[] tasks = _mailbox.GetSubscribedTasksForType<SpawnPlayerTask>(this);

            for (int i = 0; i < tasks.Length; i++)
            {
                SpawnPlayerTask task = tasks[i];
                task.IsFinished = true;
                _mailbox.RemoveTask(task);

                _charactersManager.CreatePlayer(task.Position, task.EntityName);
            }
        }

        private void HandleSpawnMonsterTasks()
        {
            SpawnMonsterTask[] tasks = _mailbox.GetSubscribedTasksForType<SpawnMonsterTask>(this);

            for (int i = 0; i < tasks.Length; i++)
            {
                SpawnMonsterTask task = tasks[i];
                task.IsFinished = true;
                _mailbox.RemoveTask(task);

                _charactersManager.CreateMonster(
                    task.Position,
                    task.EntityName,
                    _spriteLookUp.GetSprite(task.Sprite));
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
                task.IsFinished = true;
                _mailbox.RemoveTask(task);

                // FIXME: might cause performance issue (this is on a hot path)
                GameObject entity = GameObject.Find(task.EntityName);
                GameObject.Destroy(entity);
            }
        }

        private void HandleEffectTasks()
        {
            EffectTask[] tasks = _mailbox.GetSubscribedTasksForType<EffectTask>(this);

            for (int i = 0; i < tasks.Length; i++)
            {
                EffectTask task = tasks[i];
                task.IsFinished = true;
                _mailbox.RemoveTask(task);

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
                    case EffectType.Attack:
                        _effectsManager.CreateAttackEffect(task.Position);
                        break;
                }
            }
        }

        private void HandleUpdateTileItemTasks()
        {
            UpdateTileItemTask[] tasks = _mailbox.GetSubscribedTasksForType<UpdateTileItemTask>(this);

            for (int i = 0; i < tasks.Length; i++)
            {
                UpdateTileItemTask task = tasks[i];
                task.IsFinished = true;
                _mailbox.RemoveTask(task);

                if (task.Exists)
                {
                    _treasureTroveManager.PlaceTrove(task.Position.x, task.Position.y);
                }
                else
                {
                    _treasureTroveManager.RemoveTrove(task.Position.x, task.Position.y);
                }
            }
        }
    }
}
