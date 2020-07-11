using Unity.Entities;

namespace MM26.ECS
{
    abstract class TaskSystem<T> : ComponentSystem where T : Task
    {
        protected EntityQuery query;

        protected TaskSystem()
        {
        }

        protected bool ShouldFinish()
        {
            return true;
        }

        protected override void OnUpdate()
        {
            // Loop through all the entities with ids.
            // Check to see if that id has an associated task
        }
    }
}
