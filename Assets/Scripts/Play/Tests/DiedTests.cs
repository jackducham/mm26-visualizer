using System.Linq;
using UnityEngine;
using NUnit.Framework;
using MM26.IO.Models;
using MM26.ECS;
using MM26.Tasks;

namespace MM26.Play.Tests
{
    /// <summary>
    /// Test when player is dead, corresonds to <c>Died = true</c>
    /// </summary>
    [TestFixture]
    public class DiedTests
    {
        SceneConfiguration _sceneConfiguration = null;
        MockPositionLookUp _mockPositionLookUp = null;

        [SetUp]
        public void SetUp()
        {
            _sceneConfiguration = ScriptableObject.CreateInstance<SceneConfiguration>();
            _sceneConfiguration.BoardName = "test";

            _mockPositionLookUp = ScriptableObject.CreateInstance<MockPositionLookUp>();
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
                    Name = "player",
                    IsDead = true,
                    Position = new Position()
                    {
                        BoardId = playerBoard,
                        X = 0,
                        Y = 0
                    }
                }
            };

            gameChange.CharacterChanges["player"] = new CharacterChange()
            {
                Died = true,
                Respawned = false,
                Decision = new CharacterDecision()
            };

            VisualizerTurn turn = new VisualizerTurn()
            {
                Change = gameChange,
                State = gameState,
            };

            TasksBatch batch = Director.GetTasksBatch(turn, _sceneConfiguration, _mockPositionLookUp);
            DespawnTask[] tasks = batch.Tasks
                .Select(task => task as DespawnTask)
                .ToArray();

            if (playerBoard == _sceneConfiguration.BoardName)
            {
                Assert.AreEqual(1, tasks.Length);
                Assert.AreEqual("player", tasks[0].EntityName);
            }
            else
            {
                Assert.AreEqual(0, tasks.Length);
            }
        }
    }
}
