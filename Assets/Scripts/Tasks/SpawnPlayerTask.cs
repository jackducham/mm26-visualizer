using UnityEngine;
using MM26.ECS;
using MM26.Utilities;

namespace MM26.Tasks
{
    /// <summary>
    /// Sent when a new player appears on the current board
    /// </summary>
    public sealed class SpawnPlayerTask : Task
    {
        /// <summary>
        /// The position of spawning
        /// </summary>
        public readonly Vector3Int Position;
        public int Health;
        public int Level;
        public int Experience;

        public SpawnPlayerTask(string entity, Vector3Int position) : base(entity)
        {
            this.Position = position;
        }

        public override int GetHashCode()
        {
            Hash hash = Hash.Default;

            hash.Add(base.GetHashCode());
            hash.Add(this.Position.GetHashCode());
            hash.Add(this.Health.GetHashCode());
            hash.Add(this.Level.GetHashCode());
            hash.Add(this.Experience.GetHashCode());

            return hash.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SpawnPlayerTask))
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            SpawnPlayerTask task = (SpawnPlayerTask)obj;

            return this.Position == task.Position
                && this.Health == task.Health
                && this.Level == task.Level
                && this.Experience == task.Experience;
        }
    }
}
