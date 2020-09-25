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
                    Level = 17,
                    Experience = 17,
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
                    Level = 17,
                    Experience = 17,
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
                Respawned = true,
                Died = false,
                Decision = new CharacterDecision()
                {
                    DecisionType = DecisionType.None
                }
            };

            gameChange.CharacterChanges["monster"] = new CharacterChange()
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
                new HashSet<Task>()
                {
                    new EffectTask(EffectType.Spawn, new Vector3Int(1, 1, 0)),
                    new EffectTask(EffectType.Spawn, new Vector3Int(0, 1, 0)),
                    new SpawnPlayerTask("player", new Vector3Int(1, 1, 0))
                    {
                        Health = 17,
                        Level = 17,
                        Experience = 17,
                    },
                    new SpawnMonsterTask("monster", new Vector3Int(0, 1, 0), "monster.png")
                    {
                        Health = 17,
                        Level = 17,
                        Experience = 17,
                    }
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
