using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using MM26.ECS;
using MM26.IO;
using MM26.IO.Models;
using MM26.Tasks;

namespace MM26.Play.Tests
{
    public class MockPositionLookUp : Board.BoardPositionLookUp
    {
        public override Vector3 Translate(Vector3Int position)
        {
            return new Vector3(position.x, position.y, position.z);
        }
    }

    [TestFixture]
    public class MovementTests
    {
        SceneConfiguration _sceneConfiguration = null;
        MockPositionLookUp _mockPositionLookup = null;

        [SetUp]
        public void PreTest()
        {
            _sceneConfiguration = ScriptableObject.CreateInstance<SceneConfiguration>();
            _sceneConfiguration.BoardName = "test";
            _mockPositionLookup = ScriptableObject.CreateInstance<MockPositionLookUp>();
        }

        [TestCase("test")]
        [TestCase("other")]
        public void Test(string playerBoard)
        {
            // prepare data
            VisualizerTurn turn = new VisualizerTurn();

            // setup game state
            GameState gameState = new GameState();

            gameState.BoardNames["test"] = new IO.Models.Board()
            {
                Rows = 1,
                Columns = 2,
            };

            gameState.PlayerNames["player"] = new Player()
            {
                Character = new Character()
                {
                    Name = "player",
                    Position = new Position()
                    {
                        X = 0,
                        Y = 1,
                        BoardId = playerBoard
                    }
                }
            };

            GameChange gameChange = new GameChange();

            CharacterChange characterChange = new CharacterChange()
            {
                Died = false,
                Respawned = false,
            };

            characterChange.Path.Add(new Position()
            {
                X = 0,
                Y = 0,
                BoardId = playerBoard
            });

            characterChange.Path.Add(new Position()
            {
                X = 1,
                Y = 0,
                BoardId = playerBoard
            });

            gameChange.CharacterStatChanges["player"] = characterChange;

            turn.State = gameState;
            turn.Change = gameChange;

            TasksBatch batch = Director.GetTasksBatch(turn, _sceneConfiguration, _mockPositionLookup);
            List<Task> tasks = batch.Tasks;

            if (playerBoard == _sceneConfiguration.BoardName)
            {
                Assert.AreEqual(1, tasks.Count);
                Assert.AreEqual("player", tasks[0].EntityName);
                Assert.AreEqual(2, (tasks[0] as FollowPathTask).Path.Length);
            }
            else
            {
                Assert.AreEqual(0, tasks.Count);
            }
        }
    }
}