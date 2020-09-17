using UnityEngine;
using MM26.ECS;
using MM26.Utilities;

namespace MM26.Tasks
{
    public sealed class UpdateHubTask : Task
    {
        public int? Health;
        public int? Level;
        public int? Experience;

        public UpdateHubTask(string entity) : base(entity)
        {
        }

        public override int GetHashCode()
        {
            Hash hash = Hash.Default;

            hash.Add(base.GetHashCode());
            hash.Add(this.Health.GetHashCode());
            hash.Add(this.Level.GetHashCode());
            hash.Add(this.Experience.GetHashCode());

            return hash.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is UpdateHubTask))
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            UpdateHubTask other = (UpdateHubTask)obj;

            return this.Health == other.Health
                && this.Level == other.Level
                && this.Experience == other.Experience;
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
