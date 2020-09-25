using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MM26.IO.Models;
using MM26.ECS;
using MM26.Tasks;

namespace MM26.Play.Tests
{
    public class UpdateTileItemTestCases : IEnumerable
    {
        private VisualizerTurn GetTurn(string playerBoard)
        {
            var state = new GameState();

            var board = new IO.Models.Board()
            {
                Rows = 1,
                Columns = 1
            };

            var item = new Tile();
            item.Items.Add(new Item());

            board.Grid.Add(item);

            state.BoardNames[playerBoard] = board;

            var change = new GameChange();
            change.TileItemChanges.Add(new Position()
            {
                X = 0,
                Y = 0,
                BoardId = playerBoard
            });

            var turn = new VisualizerTurn()
            {
                State = state,
                Change = change
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
                    new UpdateTileItemTask(new Vector2Int(0, 0), true)
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
