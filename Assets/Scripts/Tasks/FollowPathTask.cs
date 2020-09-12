using UnityEngine;
using MM26.ECS;

namespace MM26.Tasks
{
    /// <summary>
    /// Move an entity along a specified path
    /// </summary>
    public sealed class FollowPathTask : Task
    {
        /// <summary>
        /// The path that the entity takes
        /// </summary>
        public readonly Vector3[] Path;

        public FollowPathTask(string entity, Vector3[] path) : base(entity)
        {
            this.Path = path;
        }
    }
}
