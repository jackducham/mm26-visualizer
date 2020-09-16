using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using MM26.ECS;
using MM26.IO.Models;

namespace MM26.Play.Tests
{
    [TestFixture]
    public class DirectorTests
    {
        SceneConfiguration _sceneConfiguration = null;
        MockPositionLookUp _mockPositionLookup = null;

        [SetUp]
        public void SetUp()
        {
            _sceneConfiguration = ScriptableObject.CreateInstance<SceneConfiguration>();
            _sceneConfiguration.BoardName = "test";
            _mockPositionLookup = ScriptableObject.CreateInstance<MockPositionLookUp>();
        }

        [TestCaseSource(typeof(MoveTaskCases))]
        public void Test(VisualizerTurn turn, HashSet<Task> expectedTasks)
        {
            TasksBatch batch = Director.GetTasksBatch(turn, _sceneConfiguration, _mockPositionLookup);
            var actualTasks = new HashSet<Task>(batch.Tasks);

            CollectionAssert.AreEquivalent(actualTasks, expectedTasks);
        }
    }
}
