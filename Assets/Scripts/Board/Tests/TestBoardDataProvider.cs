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
                    TileType = tile.TileType
                });
            }

            state.BoardNames.Add(_testBoard.Name, board);

            _data.GameState = state;
            _sceneLifeCycle.DataFetched.Invoke();
        }
    }
}
