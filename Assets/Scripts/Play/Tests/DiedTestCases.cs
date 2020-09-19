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
    /// Test when player is dead, corresonds to <c>Died = true</c>
    /// </summary>
    public class DiedTestCases : IEnumerable
    {
        public VisualizerTurn GetTurn(string playerBoard)
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

            return turn;
        }

        private object[] GetOnBoard()
        {
            return new object[]
            {
                this.GetTurn("test"),
                new HashSet<Task>()
                {
                    new EffectTask(EffectType.Death, new Vector3Int(0, 0, 0)),
                    new DespawnTask("player")
                }
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
