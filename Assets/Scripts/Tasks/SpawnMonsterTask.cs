using UnityEngine;
using MM26.ECS;
using MM26.Utilities;

namespace MM26.Tasks
{
    public sealed class SpawnMonsterTask : Task
    {
        public readonly Vector3Int Position;
        public Sprite MonsterSprite;

        public SpawnMonsterTask(string entity, Vector3Int position) : base(entity)
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
