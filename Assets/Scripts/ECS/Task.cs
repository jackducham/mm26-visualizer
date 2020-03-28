namespace MM26.ECS
{
    /// <summary>Class <c>Task</c>:
    /// Represents a task on a specific entity for systems to complete.
    /// </summary>
    /// <remarks>When extending this class, do not try to overload this class with too much functionality. Instead, think of it as a collection of data for a system to read.</remarks>
    ///
    public abstract class Task
    {

        private static int _nextId;
        public int entityId;

        private readonly int _taskId;

        /// <summary>
        /// Check whether a task has been marked as finished or not.
        /// </summary>
        /// <returns>True if the task is marked as finished; false otherwise.</returns>
        public bool IsFinished { get; set; }

        /// <summary>
        /// Check whether a task has been marked as started or not.
        /// </summary>
        /// <returns>True if the task is marked as started; false otherwise.</returns>
        public bool IsStarted { get; set; }

        public Task(int id)
        {
            entityId = id;
            IsFinished = false;
            _taskId = _nextId++;
        }

        /// <summary>
        /// Mark the task as having started being used. Useful for classes to know whether a task has been touched yet or not.
        /// </summary>
        public void Start()
        {
            IsStarted = true;
        }

        /// <summary>
        /// Mark a task as finished. Useful for classes to know whether a task has been finished by a system.
        /// </summary>
        /// <remarks>Will remove the task from the Mailbox system so it will no longer be returned when getting messages for a type.</remarks>
        public void Finish()
        {
            IsFinished = true;
            Mailbox.Instance.RemoveTask(this);
        }

        /// <summary>
        /// The string representation of the task type. This is required for Mailbox to properly register the task.
        /// </summary>
        /// <remarks>Make sure that this task type is unique. If you have multiple task subclasses with the same task string, errors will like occur.</remarks>
        /// <returns>The string represention of the task type.</returns>
        public abstract string GetTaskType();

        /// <summary>
        /// Get the id of the task.
        /// </summary>
        /// <returns>id of the task.</returns>
        public int GetId()
        {
            return _taskId;
        }
    }
}