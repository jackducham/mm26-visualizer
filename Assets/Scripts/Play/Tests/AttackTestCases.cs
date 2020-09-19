using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf.Collections;
using MM26.IO.Models;
using MM26.ECS;
using MM26.Tasks;


namespace MM26.Play.Tests
{
    public class AttackTestCases : IEnumerable
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
                    IsDead = false,
                    CurrentHealth = 100,
                    Level = 100,
                    Experience = 100,
                    Position = new Position()
                    {
                        BoardId = playerBoard,
                        X = 0,
                        Y = 0
                    }
                }
            };

            var playerChange = new CharacterChange()
            {
                Died = false,
                Respawned = false,
                Decision = new CharacterDecision()
                {
                    DecisionType = DecisionType.Attack,
                },
            };

            playerChange.AttackLocations.Add(new Position()
            {
                X = 0,
                Y = 0,
                BoardId = playerBoard
            });
            playerChange.AttackLocations.Add(new Position()
            {
                X = 0,
                Y = 1,
                BoardId = playerBoard
            });

            gameChange.CharacterChanges["player"] = playerChange;

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
                    new UpdateHubTask("player")
                    {
                        Experience = 100,
                        Health = 100,
                        Level = 100
                    },
                    new EffectTask(EffectType.Attack, new Vector3Int(0, 0, 0)),
                    new EffectTask(EffectType.Attack, new Vector3Int(0, 1, 0)),
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