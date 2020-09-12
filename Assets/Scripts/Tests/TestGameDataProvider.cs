﻿using UnityEngine;
using UnityEngine.Serialization;
using Google.Protobuf.Collections;
using MM26.IO;
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

        /// <summary>
        /// Event handler for fetching data
        /// </summary>
        private void OnFetchData()
        {
            _data.InitialState = this.GetState(_testData.InitialState);

            for (int i = 0; i < _testData.Turns.Length; i++)
            {
                TestGameTurn testTurn = _testData.Turns[i];
                Turn turn = new Turn(this.GetState(testTurn.State), this.GetChange(testTurn.Change));

                _data.Turns.Enqueue(turn);
            }

            _sceneLifeCycle.DataFetched.Invoke();
        }

        /// <summary>
        /// Translate from test state to state
        /// </summary>
        /// <param name="testState"></param>
        /// <returns></returns>
        private GameState GetState(TestGameState testState)
        {
            GameState state = new GameState();
            this.ConvertBoard(state, testState);
            this.ConvertPlayers(state, testState);

            return state;
        }

        /// <summary>
        /// Translate from test change to change
        /// </summary>
        /// <param name="testChange"></param>
        /// <returns></returns>
        private GameChange GetChange(TestGameChange testChange)
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

            return gameChange;
        }

        /// <summary>
        /// Helper functions for converting a board in a test state
        /// to a board in the game state
        /// </summary>
        /// <param name="state"></param>
        /// <param name="testState"></param>
        private void ConvertBoard(GameState state, TestGameState testState)
        {
            var board = new IO.Models.Board();

            board.Columns = testState.Board.Columns;
            board.Rows = testState.Board.Rows;

            foreach (var tile in testState.Board.Grid)
            {
                board.Grid.Add(new Tile()
                {
                    TileType = tile.TileType
                });
            }

            state.BoardNames.Add(testState.Board.Name, board);
        }

        /// <summary>
        /// Helper function for converting players in a test state
        /// to players in the game state
        /// </summary>
        /// <param name="state"></param>
        /// <param name="testState"></param>
        private void ConvertPlayers(GameState state, TestGameState testState)
        {
            foreach (var testPlayer in testState.Players)
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
