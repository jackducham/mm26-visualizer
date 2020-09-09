using UnityEngine;
using MM26.ECS;

namespace MM26.Tasks
{
    /// <summary>
    /// Move an entity along a specified path
    /// </summary>
    public sealed class MovementTask : Task
    {
        public readonly Vector3[] Path;

        public MovementTask(string entity, Vector3[] path) : base(entity)
        {
            this.Path = path;
        }
    }
}
