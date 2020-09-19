using UnityEngine;
using MM26.ECS;
using MM26.Utilities;

namespace MM26.Tasks
{
    public enum EffectType
    {
        Spawn,
        Death,
        Portal
    }

    public class EffectTask : Task
    {
        const string Entity = "Effect";

        public readonly EffectType Type;
        public readonly Vector3Int Position;

        public EffectTask(EffectType type, Vector3Int position) : base(EffectTask.Entity)
        {
            this.Type = type;
            this.Position = position;
        }

        public override int GetHashCode()
        {
            Hash hash = Hash.Default;
            hash.Add(this.Type.GetHashCode());
            hash.Add(this.Position.GetHashCode());

            return hash.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EffectTask))
            {
                return false;
            }

            EffectTask other = (EffectTask)obj;

            return this.Type == other.Type
                && this.Position == other.Position
                && base.Equals(obj);
        }
    }
}
