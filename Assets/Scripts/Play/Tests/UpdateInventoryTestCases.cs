﻿using System.Collections;
using System.Collections.Generic;
using MM26.IO.Models;
using MM26.ECS;
using MM26.Tasks;

namespace MM26.Play.Tests
{
    public class UpdateInventoryTestCases : IEnumerable
    {
        private VisualizerTurn GetTurn(string playerBoard)
        {
            var state = new GameState();

            state.PlayerNames["player"] = new Player()
            {
                Character = new Character()
                {
                    CurrentHealth = 10,
                    Level = 22,
                    Experience = 0,
                    Position = new Position()
                    {
                        BoardId = playerBoard,
                        X = 0,
                        Y = 0
                    }
                }
            };

            state.MonsterNames["monster"] = new Monster()
            {
                Character = new Character()
                {
                    CurrentHealth = 10,
                    Level = 22,
                    Experience = 0,
                    Position = new Position()
                    {
                        BoardId = playerBoard,
                        X = 1,
                        Y = 1
                    }
                }
            };

            var turn = new VisualizerTurn()
            {
                State = state,
                Change = new GameChange()
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
                    new UpdateInventoryTask("player")
                    {
                        hat_changed = true,
                        clothes_changed = true,

                        Top = "mm26_wearables/clothes/maid_top_alt.png",
                        Head = "mm26_wearables/hats/butler_head.png"
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
