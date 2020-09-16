using MM26.ECS;
using MM26.Utilities;

namespace MM26.Tasks
{
    public sealed class UpdateHubTask : Task
    {
        public int Health;

        public UpdateHubTask(string entity) : base(entity)
        {
        }

        public override int GetHashCode()
        {
            Hash hash = Hash.Default;

            hash.Add(base.GetHashCode());
            hash.Add(this.Health.GetHashCode());

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

            UpdateHubTask task = (UpdateHubTask)obj;

            return this.Health == task.Health;
        }
    }
}
