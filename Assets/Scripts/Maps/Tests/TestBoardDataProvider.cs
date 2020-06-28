using System;
using UnityEngine;
using MM26.IO;
using MM26.IO.Models;

namespace MM26.Map.Tests
{
    [CreateAssetMenu(menuName = "Maps/Test Board Data Provider", fileName = "TestBoardDataProvider")]
    public class TestBoardDataProvider : DataProvider
    {
        [SerializeField]
        TestBoard _testBoard = null;

        public override void Start()
        {
            base.Start();

            var state = new GameState();
            var board = new Board();

            board.Columns = _testBoard.Columns;
            board.Rows = _testBoard.Rows;

            foreach (var tile in _testBoard.Grid)
            {
                board.Grid.Add(new Tile()
                {
                    TileType = this.GetTileType(tile.TileType)
                });
            }

            state.BoardNames.Add(_testBoard.BoardName, board);

            this.Data.GameState = state;
            this.CanStart.Invoke();
        }

        private Tile.Types.TileType GetTileType(TestTileType testTileType)
        {
            switch (testTileType)
            {
                case TestTileType.Blank:
                    return Tile.Types.TileType.Blank;
                case TestTileType.Impassible:
                    return Tile.Types.TileType.Impassible;
                case TestTileType.Portal:
                    return Tile.Types.TileType.Portal;
                case TestTileType.Void:
                    return Tile.Types.TileType.Void;
                default:
                    throw new ArgumentException(string.Format("Unable to Resolve {0}", testTileType));
            }
        }
    }
}
