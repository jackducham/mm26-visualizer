using System.Linq;
using UnityEngine;
using NUnit.Framework;
using MM26.IO.Models;
using MM26.ECS;
using MM26.Tasks;

namespace MM26.Play.Tests
{
    /// <summary>
    /// Test when player telabort; corresponds to <c>DecisionType == PORTAL</c>
    /// </summary>
    [TestFixture]
    public class PortalTests
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

        [TestCase("test")]
        [TestCase("other")]
        public void Test(string playerBoard)
        {
            GameState gameState = new GameState();
            GameChange gameChange = new GameChange();

            gameState.BoardNames["test"] = new IO.Models.Board()
            {
                Rows = 2,
                Columns = 2
            };

            gameState.PlayerNames["player"] = new Player()
            {
                Character = new Character()
                {
                    Position = new Position()
                    {
                        BoardId = playerBoard,
                        X = 1,
                        Y = 1
                    }
                }
            };

            gameChange.CharacterChanges["player"] = new CharacterChange()
            {
                Died = false,
                Respawned = false,
                Decision = new CharacterDecision()
                {
                    DecisionType = DecisionType.Portal
                }
            };

            VisualizerTurn turn = new VisualizerTurn()
            {
                State = gameState,
                Change = gameChange,
            };

            TasksBatch batch = Director.GetTasksBatch(turn, _sceneConfiguration, _mockPositionLookup);
            SpawnTask[] tasks = batch.Tasks
                .Select(task => task as SpawnTask)
                .ToArray();

            if (playerBoard == _sceneConfiguration.BoardName)
            {
                Assert.AreEqual(1, tasks.Length);
                Assert.AreEqual(new Vector3Int(1, 1, 0), tasks[0].Position);
            }
            else
            {
                Assert.AreEqual(0, tasks.Length);
            }
        }
    }
}