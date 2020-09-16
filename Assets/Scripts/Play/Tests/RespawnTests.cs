using System.Linq;
using UnityEngine;
using NUnit.Framework;
using MM26.IO.Models;
using MM26.ECS;
using MM26.Tasks;

namespace MM26.Play.Tests
{
    /// <summary>
    /// Test when player is dead, corresonds to <c>Respawned = true</c>
    /// </summary>
    [TestFixture]
    public class RespawnTests
    {
        SceneConfiguration _sceneConfiguration = null;
        MockPositionLookUp _mockPositinoLookUp = null;

        [SetUp]
        public void SetUp()
        {
            _sceneConfiguration = ScriptableObject.CreateInstance<SceneConfiguration>();
            _sceneConfiguration.BoardName = "test";

            _mockPositinoLookUp = ScriptableObject.CreateInstance<MockPositionLookUp>();
        }

        [TestCase("test")]
        [TestCase("other")]
        public void Test(string playerBoard)
        {
            GameState gameState = new GameState();
            GameChange gameChange = new GameChange();

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
                Respawned = true,
                Died = false,
                Decision = new CharacterDecision()
                {
                    DecisionType = DecisionType.None
                }
            };

            VisualizerTurn turn = new VisualizerTurn()
            {
                Change = gameChange,
                State = gameState,
            };

            TasksBatch batch = Director.GetTasksBatch(turn, _sceneConfiguration, _mockPositinoLookUp);
            SpawnTask[] tasks = batch
                .Select(task => task as SpawnTask)
                .ToArray();

            if (playerBoard == _sceneConfiguration.BoardName)
            {
                Assert.AreEqual(1, tasks.Length);
                Assert.AreEqual("player", tasks[0].EntityName);
                Assert.AreEqual(new Vector3Int(1, 1, 0), tasks[0].Position);
            }
            else
            {
                Assert.AreEqual(0, tasks.Length);
            }
        }
    }
}
