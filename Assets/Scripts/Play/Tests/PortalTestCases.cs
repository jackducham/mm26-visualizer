using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MM26.ECS;
using MM26.IO.Models;
using MM26.Tasks;

namespace MM26.Play.Tests
{
    public class PortalTestCases : IEnumerable
    {
        /// <summary>
        /// Make turn for a given board
        /// </summary>
        /// <param name="playerBoard">the board to make for</param>
        /// <returns></returns>
        private VisualizerTurn GetBoard(string playerBoard)
        {
            GameState gameState = new GameState();
            GameChange gameChange = new GameChange();

            gameState.BoardNames[playerBoard] = new IO.Models.Board()
            {
                Width = 2,
                Height = 2
            };

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

            gameState.MonsterNames["monster"] = new Monster()
            {
                Character = new Character()
                {
                    CurrentHealth = 17,
                    Sprite = "monster.png",
                    Position = new Position()
                    {
                        BoardId = playerBoard,
                        X = 0,
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

            gameChange.CharacterChanges["monster"] = new CharacterChange()
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

            return turn;
        }

        private object[] GetOnBoard()
        {
            return new object[]
            {
                this.GetBoard("test"),
                new HashSet<Task>()
                {
                    new EffectTask(EffectType.Portal, new Vector3Int(1, 1, 0)),
                    new EffectTask(EffectType.Portal, new Vector3Int(0, 1, 0)),
                    new SpawnPlayerTask("player", new Vector3Int(1, 1, 0)),
                    new SpawnMonsterTask("monster", new Vector3Int(0, 1, 0), "monster.png")
                }
            };
        }

        private object[] GetNotOnBOard()
        {
            return new object[]
            {
                this.GetBoard("other"),
                new HashSet<Task>()
                {
                    new EffectTask(EffectType.Portal, new Vector3Int(1, 1, 0)),
                    new EffectTask(EffectType.Portal, new Vector3Int(0, 1, 0)),
                    new DespawnTask("player"),
                    new DespawnTask("monster")
                }
            };
        }

        public IEnumerator GetEnumerator()
        {
            yield return this.GetOnBoard();
            yield return this.GetNotOnBOard();
        }
    }

}
