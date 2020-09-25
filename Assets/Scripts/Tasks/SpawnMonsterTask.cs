using UnityEngine;
using MM26.ECS;
using MM26.Utilities;

namespace MM26.Tasks
{
    public sealed class SpawnMonsterTask : Task
    {
        public readonly Vector3Int Position;
        public string Sprite;
        public int Health;
        public int Level;
        public int Experience;

        public SpawnMonsterTask(string entity, Vector3Int position, string sprite) : base(entity)
        {
            this.Position = position;
            this.Sprite = sprite;
        }

        public override int GetHashCode()
        {
            Hash hash = Hash.Default;

            hash.Add(base.GetHashCode());
            hash.Add(this.Position.GetHashCode());
            hash.Add(this.Sprite.GetHashCode());
            hash.Add(this.Health.GetHashCode());
            hash.Add(this.Level.GetHashCode());
            hash.Add(this.Experience.GetHashCode());

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

            return this.Position == task.Position
                && this.Sprite == task.Sprite
                && this.Health == task.Health
                && this.Level == task.Level
                && this.Experience == task.Experience;
        }
    }
}
