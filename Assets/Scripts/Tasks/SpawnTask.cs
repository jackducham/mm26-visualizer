using UnityEngine;
using MM26.ECS;

namespace MM26.Tasks
{
    /// <summary>
    /// Sent when a new player appears on the current board
    /// </summary>
    public sealed class SpawnTask : Task
    {
        public readonly Vector2Int Position;

        public SpawnTask(string entity, Vector2Int position) : base(entity)
        {
            this.Position = position;
        }
    }
}
