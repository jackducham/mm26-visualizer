﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MM26.IO.Models;

namespace MM26.Tests.Scenes
{
    public class DelayedBoardLoadingScene : MonoBehaviour
    {
        [SerializeField]
        IO.Data _data = null;

        [SerializeField]
        SceneLifeCycle _lifeCycle = null;

        private void Start()
        {
            var initial = new VisualizerInitial();
            var state = new GameState();

            var board = new IO.Models.Board()
            {
                Height = 1,
                Width = 1
            };

            board.Grid.Add(new Tile()
            {
                GroundSprite = "mm26_tiles/dirt1.png",
                TileType = Tile.Types.TileType.Blank,
            });

            state.BoardNames["pvp"] = board;

            initial.State = state;
            _data.Initial = initial;

            _lifeCycle.FetchData.Invoke();
            _lifeCycle.DataFetched.Invoke();

            StartCoroutine(this.Simulate(board));
        }

        private IEnumerator Simulate(IO.Models.Board board)
        {
            yield return new WaitForSecondsRealtime(2.0f);

            var turn1State = new GameState();
            turn1State.BoardNames["player"] = board;

            _data.Turns.Enqueue(new VisualizerTurn()
            {
                State = turn1State
            });

            _data.Turns.Enqueue(new VisualizerTurn()
            {
                State = turn1State
            });
        }
    }
}
