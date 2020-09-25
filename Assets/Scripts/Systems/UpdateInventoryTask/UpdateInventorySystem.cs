using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using MM26.Components;
using MM26.ECS;
using MM26.Tasks;
using MM26.Configuration;

namespace MM26.Systems.UpdateInventoryTask
{
    public class UpdateInventorySystem : SystemBase
    {
        private Mailbox _mailbox;
        private SpriteLookUp _spriteDatabase;
        private Dictionary<string, Tasks.UpdateInventoryTask> _tasks;

        protected override void OnCreate()
        {
            base.OnCreate();

            _mailbox = Resources.Load<Mailbox>("Objects/Mailbox");
            _mailbox.SubscribeToTaskType<Tasks.UpdateInventoryTask>(this);

            _spriteDatabase = Resources.Load<SpriteLookUp>("Objects/SpriteLookUp");

            _tasks = new Dictionary<string, Tasks.UpdateInventoryTask>();
        }

        protected override void OnUpdate()
        {
            _tasks.Clear();

            Tasks.UpdateInventoryTask[] tasks = _mailbox.GetSubscribedTasksForType<Tasks.UpdateInventoryTask>(this);

            for (int i = 0; i < tasks.Length; i++)
            {
                Tasks.UpdateInventoryTask updateInventoryTask = tasks[i];
                _tasks[updateInventoryTask.EntityName] = updateInventoryTask;
                _mailbox.RemoveTask(updateInventoryTask);
            }

            this.Entities
                .WithoutBurst()
                .ForEach((Character character, Inventory inventory) =>
                {
                    if (_tasks.TryGetValue(character.name, out Tasks.UpdateInventoryTask task))
                    {
                        if (task.shoes_changed)
                        {
                            inventory.Bottom = _spriteDatabase.GetWearable(task.Bottom);
                        }

                        if(task.clothes_changed)
                        {
                            inventory.Top = _spriteDatabase.GetWearable(task.Top);
                        }

                        if(task.hat_changed)
                        {
                            inventory.Head = _spriteDatabase.GetWearable(task.Head);
                        }

                        if(task.accesory_changed)
                        {
                            inventory.Accessory = _spriteDatabase.GetWearable(task.Accessory);
                        }

                        if(task.weapon_changed)
                        {
                            inventory.Weapon = _spriteDatabase.GetWearable(task.Weapon);
                        }

                        task.IsFinished = true;
                    }
                })
                .Run();
        }
    }
}