using MM26.ECS;

namespace MM26.Tasks
{
    /// <summary>
    /// Sent when a new player disappears from the current board
    /// </summary>
    public sealed class DespawnTask : Task
    {
        public DespawnTask(string entity) : base(entity)
        {
        }
    }
}
