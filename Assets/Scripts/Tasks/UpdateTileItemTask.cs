using UnityEngine;
using MM26.ECS;
using MM26.Utilities;

namespace MM26.Tasks
{
    public class UpdateTileItemTask : Task
    {
        public const string Entity = "Item";

        public Vector2Int Position;
        public bool Exists;

        public UpdateTileItemTask(Vector2Int position, bool exists) : base(Entity)
        {
            this.Position = position;
            this.Exists = exists;
        }

        public override int GetHashCode()
        {
            Hash hash = Hash.Default;

            hash.Add(base.GetHashCode());
            hash.Add(this.Position.GetHashCode());
            hash.Add(this.Exists.GetHashCode());

            return hash.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is UpdateTileItemTask))
            {
                return false;
            }

            UpdateTileItemTask other = (UpdateTileItemTask)obj;

            return base.Equals(other)
                && this.Position == other.Position
                && this.Exists == other.Exists;
        }
    }
}
