using UnityEngine;
using MM26.ECS;

namespace MM26.Tasks
{
    public class MovementTask : Task
    {
        public Transform[] Path = null;

        public MovementTask(
            string entity,
            Mailbox mailbox,
            Transform[] path) : base(entity, mailbox)
        {
            this.Path = path;
        }
    }
}
