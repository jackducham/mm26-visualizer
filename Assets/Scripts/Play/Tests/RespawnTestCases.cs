using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MM26.IO.Models;
using MM26.ECS;
using MM26.Tasks;

namespace MM26.Play.Tests
{
    /// <summary>
    /// Test when player is dead, corresonds to <c>Respawned = true</c>
    /// </summary>
    public class RespawnTestCases : IEnumerable
    {
        private VisualizerTurn GetTurn(string playerBoard)
        {
            GameState gameState = new GameState();
            GameChange gameChange = new GameChange();

            gameState.PlayerNames["player"] = new Player()
            {
                Character = new Character()
                {
                    CurrentHealth = 17,
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

            return turn;
        }

        private object[] GetOnBoard()
        {
            return new object[]
            {
                this.GetTurn("test"),
                new HashSet<Task>(),
            };
        }

        private object[] GetNotOnBoard()
        {
            return new object[]
            {
                this.GetTurn("other"),
                new HashSet<Task>()
            };
        }

        public IEnumerator GetEnumerator()
        {
            yield return this.GetOnBoard();
            yield return this.GetNotOnBoard();
        }
    }
}
