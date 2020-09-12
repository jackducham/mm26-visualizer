using UnityEngine;
using NUnit.Framework;
using MM26.ECS;
using MM26.IO;
using MM26.IO.Models;
using MM26.Tasks;

namespace MM26.Play.Tests
{
    public class MockPositionLookUp
    {

    }

    [TestFixture]
    public class MovementTests
    {
        SceneConfiguration _sceneConfiguration = null;
        SceneLifeCycle _sceneLifeCycle = null;

        TasksManager _taskManager = null;
        Mailbox _mailbox = null;
        Data _data = null;

        Director _director;

        [SetUp]
        public void PreTest()
        {
            _sceneConfiguration = ScriptableObject.CreateInstance<SceneConfiguration>();
            _sceneConfiguration.BoardName = "test";
            _sceneLifeCycle = ScriptableObject.CreateInstance<SceneLifeCycle>();

            _taskManager = ScriptableObject.CreateInstance<TasksManager>();
            _mailbox = ScriptableObject.CreateInstance<Mailbox>();
            _data = ScriptableObject.CreateInstance<Data>();

            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
            Director director = gameObject.AddComponent<Director>();

            _director = director;

            _director.Data = _data;
            _director.TaskManager = _taskManager;
            _director.SceneConfiguration = _sceneConfiguration;
            _director.SceneLifeCycle = _sceneLifeCycle;

            Assert.NotNull(_director.Data);
            Assert.NotNull(_director.TaskManager);
            Assert.NotNull(_director.SceneConfiguration);
            Assert.NotNull(_director.SceneLifeCycle);
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

            _data.Turns.Enqueue(turn);

            // run director
            _director.DispatchTasks();

            var tasks = _mailbox.GetSubscribedTasksForType<FollowPathTask>(_director);

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