using UnityEngine;
using MM26.ECS;
using MM26.Tasks;

namespace MM26.Board
{
    public class BoardUpdator : MonoBehaviour
    {
        [SerializeField]
        internal CharactersManager CharactersManager = null;

        [SerializeField]
        internal Mailbox Mailbox = null;

        private void OnEnable()
        {
            this.Mailbox.SubscribeToTaskType<SpawnPlayerTask>(this);
            this.Mailbox.SubscribeToTaskType<DespawnTask>(this);
        }

        private void Update()
        {
            this.HandleSpawnTasks();
            this.HandleDespawnTasks();
        }

        /// <summary>
        /// Helper function to handle spawn tasks
        ///
        /// 
        /// </summary>
        private void HandleSpawnTasks()
        {
            Task[] tasks = this.Mailbox.GetSubscribedTasksForType<SpawnPlayerTask>(this);

            for (int i = 0; i < tasks.Length; i++)
            {
                SpawnPlayerTask task = tasks[i] as SpawnPlayerTask;

                this.CharactersManager.CreatePlayer(task.Position, task.EntityName);

                task.IsFinished = true;
                this.Mailbox.RemoveTask(task);
            }
        }

        /// <summary>
        /// Helper function to handle despawn tasks
        /// 
        /// <b>Noe that this is called once per frame</b>
        /// </summary>
        private void HandleDespawnTasks()
        {
            Task[] tasks = this.Mailbox.GetSubscribedTasksForType<DespawnTask>(this);

            for (int i = 0; i < tasks.Length; i++)
            {
                DespawnTask task = tasks[i] as DespawnTask;

                // FIXME: might cause performance issue (this is on a hot path)
                GameObject entity = GameObject.Find(task.EntityName);
                GameObject.Destroy(entity);

                task.IsFinished = true;
                this.Mailbox.RemoveTask(task);
            }
        }
    }
}
