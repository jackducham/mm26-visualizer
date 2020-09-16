using MM26.ECS;

namespace MM26.Tasks
{
    public sealed class UpdateHubTask : Task
    {
        public int? Health;

        public UpdateHubTask(string entity) : base(entity)
        {
        }
    }
}
