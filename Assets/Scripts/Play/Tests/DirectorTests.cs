using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using MM26.ECS;
using MM26.IO.Models;

namespace MM26.Play.Tests
{
    /// <summary>
    /// Board name: "test"
    /// </summary>
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
        [TestCaseSource(typeof(PortalTestCases))]
        [TestCaseSource(typeof(DiedTestCases))]
        [TestCaseSource(typeof(RespawnTestCases))]
        [TestCaseSource(typeof(UpdateHubTestCases))]
        [TestCaseSource(typeof(AttackTestCases))]
        public void Test(VisualizerTurn turn, HashSet<Task> expectedTasks)
        {
            TasksBatch batch = turn.ToTasksBatch(_sceneConfiguration, _mockPositionLookup);
            var actualTasks = new HashSet<Task>(batch.Tasks);

            CollectionAssert.AreEquivalent(expectedTasks, actualTasks);
        }
    }
}
