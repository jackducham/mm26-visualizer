using UnityEngine;
using MM26.ECS;
using MM26.Utilities;

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

        public override int GetHashCode()
        {
            Hash hash = Hash.Default;

            hash.Add(base.GetHashCode());

            for (int i = 0; i < this.Path.Length; i++)
            {
                hash.Add(this.Path[i].GetHashCode());
            }

            return hash.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is FollowPathTask))
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            FollowPathTask task = (FollowPathTask)obj;

            if (task.Path.Length != task.Path.Length)
            {
                return false;
            }

            for (int i = 0; i < this.Path.Length; i++)
            {
                if (this.Path[i] != task.Path[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
