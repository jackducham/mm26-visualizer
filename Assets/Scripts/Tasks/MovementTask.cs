using UnityEngine;
using MM26.ECS;

namespace MM26.Tasks
{
    public class MovementTask : Task
    {
        public static string Type => "MovementTask";

        public Transform Destination;

        public MovementTask(int id, Transform destination): base(id)
        {
            this.Destination = destination;
        }

        public override string GetTaskType()
        {
            return "";
        }
    }
}
