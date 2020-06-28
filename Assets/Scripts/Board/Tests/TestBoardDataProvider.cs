using System;
using UnityEngine;
using MM26.IO.Models;

namespace MM26.Board.Tests
{
    public class TestBoardDataProvider : MonoBehaviour
    {
        [SerializeField]
        IO.Data _data = null;

        [SerializeField]
        SceneLifeCycle _sceneLifeCycle = null;

        [SerializeField]
        TestBoard _testBoard = null;

        private void OnEnable()
        {
            _sceneLifeCycle.FetchData.AddListener(this.OnFetchData);
        }

        private void OnDisable()
        {
            _sceneLifeCycle.FetchData.RemoveListener(this.OnFetchData);
        }

        private void OnFetchData()
        {
            var state = new GameState();
            var board = new IO.Models.Board();

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

            _data.GameState = state;
            _sceneLifeCycle.DataFetched.Invoke();
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
