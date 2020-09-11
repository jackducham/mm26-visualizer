using UnityEngine;
using UnityEngine.Serialization;
using Google.Protobuf.Collections;
using MM26.IO.Models;

namespace MM26.Tests
{
    using PPlayer = MM26.IO.Models.Player;
    using PCharacter = MM26.IO.Models.Character;
    using PPosition = MM26.IO.Models.Position;

    /// <summary>
    /// Mock data provider
    /// </summary>
    public class TestGameDataProvider : MonoBehaviour
    {
        [Header("Scene Essentials")]
        [SerializeField]
        IO.Data _data = null;

        [SerializeField]
        SceneLifeCycle _sceneLifeCycle = null;

        [Header("Scene Specific")]
        [FormerlySerializedAs("_testGameState")]
        [SerializeField]
        TestGameData _testData = null;

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
            this.FetchState();
            this.FetchChange();
            _sceneLifeCycle.DataFetched.Invoke();
        }

        private void FetchState()
        {
            var state = new GameState();

            this.FetchBoard(state);
            this.FetchPlayers(state);

            _data.GameState = state;
        }

        private void FetchBoard(GameState state)
        {
            var board = new IO.Models.Board();

            board.Columns = _testData.State.Board.Columns;
            board.Rows = _testData.State.Board.Rows;

            foreach (var tile in _testData.State.Board.Grid)
            {
                board.Grid.Add(new Tile()
                {
                    TileType = tile.TileType
                });
            }

            state.BoardNames.Add(_testData.State.Board.Name, board);
        }

        private void FetchPlayers(GameState state)
        {
            foreach (var testPlayer in _testData.State.Players)
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

        private void FetchChange()
        {
            foreach (TestGameChange testChange in _testData.Changes)
            {
                var gameChange = new GameChange();

                foreach (TestCharacterChange testCharacterChange in testChange.CharacterChanges)
                {
                    var characterChange = new CharacterChange();
                    characterChange.Died = testCharacterChange.Died;
                    characterChange.Respawned = testCharacterChange.Respawned;

                    if (testCharacterChange.Path != null)
                    {
                        foreach (var testPosition in testCharacterChange.Path)
                        {
                            characterChange.Path.Add(new PPosition()
                            {
                                X = testPosition.X,
                                Y = testPosition.Y,
                                BoardId = testPosition.BoardID
                            });
                        }
                    }

                    gameChange.CharacterStatChanges.Add(
                        testCharacterChange.Entity,
                        characterChange);
                }

                _data.GameChanges.Add(gameChange);
            }
        }
    }
}
