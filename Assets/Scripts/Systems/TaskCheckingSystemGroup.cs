using Unity.Entities;

namespace MM26.Systems
{
    [UpdateAfter(typeof(TaskExecutionSystemGroup))]
    public class TaskCheckingSystemGroup : ComponentSystemGroup
    {
    }
}
