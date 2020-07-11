using UnityEngine;
using MM26.ECS;

namespace MM26.Tasks
{
    public class MovementTask : Task
    {
        public static string Type => "Movement";

        public Transform Destination;

        public MovementTask(int id, Mailbox mailbox, Transform destination) : base(id, mailbox)
        {
            this.Destination = destination;
        }

        public override string GetTaskType()
        {
            return MovementTask.Type;
        }
    }
}
