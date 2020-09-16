using UnityEngine;
using MM26.ECS;
using MM26.Utilities;

namespace MM26.Tasks
{
    /// <summary>
    /// Sent when a new player appears on the current board
    /// </summary>
    public sealed class SpawnTask : Task
    {
        /// <summary>
        /// The position of spawning
        /// </summary>
        public readonly Vector3Int Position;

        public SpawnTask(string entity, Vector3Int position) : base(entity)
        {
            this.Position = position;
        }

        public override int GetHashCode()
        {
            Hash hash = Hash.Default;

            hash.Add(base.GetHashCode());
            hash.Add(this.Position.GetHashCode());

            return hash.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SpawnMonsterTask))
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            SpawnMonsterTask task = (SpawnMonsterTask)obj;

            return this.Position == task.Position;
        }
    }
}
