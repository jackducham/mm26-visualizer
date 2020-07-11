using System;
using UnityEngine;
using MM26.IO.Models;

namespace MM26.Board.Tests
{
    using PPlayer = MM26.IO.Models.Player;
    using PCharacter = MM26.IO.Models.Character;
    using PPosition = MM26.IO.Models.Position;

    public class TestGameStateDataProvider : MonoBehaviour
    {
        [SerializeField]
        IO.Data _data = null;

        [SerializeField]
        SceneLifeCycle _sceneLifeCycle = null;

        [SerializeField]
        TestGameState _testGameState = null;

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

            this.FetchBoard(state);
            this.FetchPlayers(state);

            _data.GameState = state;
            _sceneLifeCycle.DataFetched.Invoke();
        }

        private void FetchBoard(GameState state)
        {
            var board = new IO.Models.Board();

            board.Columns = _testGameState.Board.Columns;
            board.Rows = _testGameState.Board.Rows;

            foreach (var tile in _testGameState.Board.Grid)
            {
                board.Grid.Add(new Tile()
                {
                    TileType = tile.TileType
                });
            }

            state.BoardNames.Add(_testGameState.Board.Name, board);
        }

        private void FetchPlayers(GameState state)
        {
            foreach (var testPlayer in _testGameState.Players)
            {
                var player = new PPlayer()
                {
                    Character = new PCharacter()
                    {
                        Name = testPlayer.Name,
                        Position = new PPosition
                        {
                            BoardId = testPlayer.Board,
                            X = testPlayer.X,
                            Y = testPlayer.Y
                        }
                    }
                };

                state.PlayerNames.Add(testPlayer.Name, player);
            }
        }
    }
}
