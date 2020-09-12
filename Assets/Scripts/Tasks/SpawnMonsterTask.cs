using UnityEngine;
using MM26.ECS;

namespace MM26.Tasks
{
    public sealed class SpawnMonsterTask : Task
    {
        public readonly Vector3Int Position;

        public SpawnMonsterTask(string entity, Vector3Int position) : base(entity)
        {
            this.Position = position;
        }
    }
}
