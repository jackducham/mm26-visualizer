using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NUnit.Framework;


namespace MM26.ECS.Tests
{
    public class MailboxTests
    {
        private class PositionTask : Task
        {
            public PositionTask(string entity) : base(entity)
            {
            }
        }

        private class RotationTask : Task
        {
            public RotationTask(string entity) : base(entity)
            {
            }
        }

        private class GarbageTask : Task
        {
            public GarbageTask(string entity) : base(entity)
            {
            }
        }

        /// <summary>
        /// When there is no tasks in the mailbox
        /// </summary>
        [Test]
        public void TestNoTasks()
        {
            var mailbox = ScriptableObject.CreateInstance<Mailbox>();

            mailbox.SubscribeToTaskType<PositionTask>(this);
            mailbox.SubscribeToTaskType<RotationTask>(this);

            Task[] positionTasks = mailbox.GetSubscribedTasksForType<PositionTask>(this);
            Task[] rotationTasks = mailbox.GetSubscribedTasksForType<PositionTask>(this);

            Assert.NotNull(positionTasks);
            Assert.NotNull(rotationTasks);
        }

        /// <summary>
        /// Make sure that tasks can be sent regularly
        /// </summary>
        [Test]
        public void TaskSendTasks()
        {
            var mailbox = ScriptableObject.CreateInstance<Mailbox>();

            mailbox.SubscribeToTaskType<PositionTask>(this);
            mailbox.SubscribeToTaskType<RotationTask>(this);

            mailbox.SendTask(new PositionTask("test-0"));
            mailbox.SendTask(new RotationTask("test-0"));
            mailbox.SendTask(new GarbageTask("test-0"));

            mailbox.SendTask(new PositionTask("test-1"));
            mailbox.SendTask(new RotationTask("test-1"));
            mailbox.SendTask(new GarbageTask("test-1"));

            PositionTask[] positionTasks = mailbox.GetSubscribedTasksForType<PositionTask>(this)
                .Select((task) => { return task as PositionTask; })
                .ToArray();

            Assert.AreEqual(2, positionTasks.Length);
            Assert.AreEqual(positionTasks[0].EntityName, "test-0");
            Assert.AreEqual(positionTasks[1].EntityName, "test-1");

            RotationTask[] rotationTasks = mailbox.GetSubscribedTasksForType<RotationTask>(this)
                .Select((task) => { return task as RotationTask; })
                .ToArray();

            Assert.AreEqual(2, rotationTasks.Length);
            Assert.AreEqual("test-0", rotationTasks[0].EntityName);
            Assert.AreEqual("test-1", rotationTasks[1].EntityName);
        }

        /// <summary>
        /// Make sure <c>Update</c> method of Mailbox works as expected
        /// </summary>
        [Test]
        public void TestUpdate()
        {
            var mailbox = ScriptableObject.CreateInstance<Mailbox>();

            mailbox.SubscribeToTaskType<RotationTask>(this);

            mailbox.SendTask(new PositionTask("test-0"));
            mailbox.SendTask(new RotationTask("test-0"));
            mailbox.SendTask(new GarbageTask("test-0"));

            var taskToFinish = new RotationTask("test-1");

            mailbox.SendTask(new PositionTask("test-1"));
            mailbox.SendTask(taskToFinish);
            mailbox.SendTask(new GarbageTask("test-1"));

            for (int i = 0; i < 10; i++)
            {
                mailbox.Update();
            }

            taskToFinish.IsFinished = true;
            mailbox.RemoveTask(taskToFinish);

            mailbox.Update();

            RotationTask[] rotationTasks = mailbox.GetSubscribedTasksForType<RotationTask>(this)
                .Select(task => { return task as RotationTask; })
                .ToArray();

            Assert.AreEqual(1, rotationTasks.Length);
            Assert.AreEqual("test-0", rotationTasks[0].EntityName);
        }
    }
}
