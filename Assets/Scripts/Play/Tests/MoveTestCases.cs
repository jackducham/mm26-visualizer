using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MM26.ECS;
using MM26.Tasks;
using MM26.IO.Models;

namespace MM26.Play.Tests
{
    public class MoveTaskCases : IEnumerable
    {
        public object[] GetCase()
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
                    CurrentHealth = 17,
                    Position = new Position()
                    {
                        X = 0,
                        Y = 1,
                        BoardId = "test"
                    }
                }
            };

            GameChange gameChange = new GameChange();

            CharacterChange characterChange = new CharacterChange()
            {
                Died = false,
                Respawned = false,
                Decision = new CharacterDecision()
                {
                    DecisionType = DecisionType.Move
                }
            };

            characterChange.Path.Add(new Position()
            {
                X = 0,
                Y = 0,
                BoardId = "test"
            });

            characterChange.Path.Add(new Position()
            {
                X = 1,
                Y = 0,
                BoardId = "test"
            });

            gameChange.CharacterChanges["player"] = characterChange;

            turn.State = gameState;
            turn.Change = gameChange;

            var expectedTasks = new HashSet<Task>()
            {
                new FollowPathTask(
                    "player",
                    new Vector3[] { new Vector3(0, 0), new Vector3(1, 0) }),
                new UpdateHubTask("player")
                {
                    Health = 17
                }
            };

            return new object[] { turn, expectedTasks };
        }

        // Start is called before the first frame update
        public IEnumerator GetEnumerator()
        {
            yield return this.GetCase();
        }
    }
}