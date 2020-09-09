using UnityEngine;
using MM26.ECS;

namespace MM26.Tasks
{
    public sealed class MovementTask : Task
    {
        public Vector3[] Path = null;

        public MovementTask(string entity, Vector3[] path) : base(entity)
        {
            this.Path = path;
        }
    }
}
