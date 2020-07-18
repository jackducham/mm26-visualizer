using UnityEngine;
using MM26.ECS;

namespace MM26.Tasks
{
    public class MovementTask : Task
    {
        public static string Type => "Movement";

        public Transform Destination;

        public MovementTask(
            string entityName,
            Mailbox mailbox,
            Transform destination) : base(entityName, mailbox)
        {
            this.Destination = destination;
        }
    }
}
