﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Tilemaps;
using MM26.Components;
using MM26.ECS;
using MM26.Tasks;
using MM26.Configuration;

namespace MM26.Board
{
    using PTileType = MM26.IO.Models.Tile.Types.TileType;
    using PTile = MM26.IO.Models.Tile;
    using PCharacter = MM26.IO.Models.Character;
    using PPlayer = MM26.IO.Models.Player;
    using PPosition = MM26.IO.Models.Position;

    /// <summary>
    /// Responsible for creating and managing the board
    /// </summary>
    public sealed class BoardCreator : MonoBehaviour
    {
        [Header("Tiles")]
        [SerializeField]
        private SpriteLookUp _tileDatabase = null;

        [Header("Scene Specific")]
        [SerializeField]
        private SceneConfiguration _sceneConfiguration = null;

        [Header("Scene Essentials")]
        [SerializeField]
        private SceneLifeCycle _sceneLifeCycle = null;

        [SerializeField]
        private IO.Data _data = null;

        [SerializeField]
        private BoardPositionLookUp _positionLookUp = null;

        [Header("UI")]
        [SerializeField]
        private Canvas _waitingCanvas = null;

        [Header("Others")]
        [SerializeField]
        private Grid _grid = null;

        [SerializeField]
        private Tilemap _aboveTilemap = null;

        [SerializeField]
        private Tilemap _groundTilemap = null;

        [SerializeField]
        private CharactersManager _charactersManager = null;

        [SerializeField]
        private TreasureTrovesManager _treasureTrovesManager = null;

        private bool _loaded = false;

        private void Awake()
        {
            _waitingCanvas.enabled = true;
        }

        private void OnEnable()
        {
            _sceneLifeCycle.CreateBoard.AddListener(this.OnCreateMap);
        }

        private void OnDisable()
        {
            _sceneLifeCycle.CreateBoard.RemoveListener(this.OnCreateMap);
        }

        private void Update()
        {
            if (!_loaded && _data.Turns.Count > 0)
            {
                IO.Models.GameState state = _data.Turns.Peek().State;

                if (state.BoardNames.ContainsKey(_sceneConfiguration.BoardName))
                {
                    _loaded = true;
                    _waitingCanvas.enabled = false;

                    this.CreateMap(state);
                    this.CreateCharacters(state);

                    Debug.Log("Actually create board");
                }
            }
        }

        /// <summary>
        /// Event handler for <c>_sceneLifeCycle.CreateBoard</c>
        /// </summary>
        private void OnCreateMap()
        {
            if (_data.Initial.State.BoardNames.ContainsKey(_sceneConfiguration.BoardName))
            {
                this.CreateMap(_data.Initial.State);
                this.CreateCharacters(_data.Initial.State);

                _loaded = true;
                _waitingCanvas.enabled = false;

                Debug.Log(_data.Initial.State.BoardNames.Keys);
            }

            _sceneLifeCycle.BoardCreated.Invoke();
        }

        /// <summary>
        /// Helper function for creating the board
        /// </summary>
        private void CreateMap(IO.Models.GameState state)
        {
            var board = state.BoardNames[_sceneConfiguration.BoardName];

            _positionLookUp.Grid = _grid;
            _positionLookUp.Height = board.Height;
            _positionLookUp.Width = board.Width;

            _treasureTrovesManager.mapHeight = board.Height;

            for (int x = 0; x < board.Width; x++)
            {
                for (int y = 0; y < board.Height; y++)
                {
                    PTile ptile = board.Grid[x * board.Height + y];

                    if (ptile.Items.Count > 0)
                    {
                        _treasureTrovesManager.PlaceTrove(x, y);
                    }

                    if (ptile.TileType == PTileType.Void)
                    {
                        continue;
                    }

                    if (ptile.GroundSprite != "")
                    {
                        Tile tile = _tileDatabase.GetTile(ptile.GroundSprite);
                        _groundTilemap.SetTile(new Vector3Int(x, (board.Height - 1) - y, 0), tile);
                    }

                    if (ptile.AboveSprite != "")
                    {
                        Tile tile = _tileDatabase.GetTile(ptile.AboveSprite);
                        _aboveTilemap.SetTile(new Vector3Int(x, (board.Height - 1) - y, -1), tile);
                    }
                }
            }
        }

        /// <summary>
        /// Helper function for creating players
        /// </summary>
        private void CreateCharacters(IO.Models.GameState state)
        {
            foreach (var entry in state.PlayerNames)
            {
                PPlayer pplayer = entry.Value;
                PCharacter character = entry.Value.Character;
                PPosition position = character.Position;

                if (position.BoardId != _sceneConfiguration.BoardName)
                {
                    continue;
                }

                GameObject player = this._charactersManager.CreatePlayer(
                    new Vector3Int(position.X, position.Y, 0),
                    character.Name);

                var hub = player.GetComponent<Hub>();

                hub.Level = character.Level;
                hub.Health = character.CurrentHealth;
                hub.Experience = character.Experience;

                var inventory = player.GetComponent<Inventory>();

                if (inventory != null)
                {
                    Sprite head = _tileDatabase.GetWearable(pplayer.Hat?.Sprite);
                    if (head != null)
                        inventory.Head = head;

                    Sprite top = _tileDatabase.GetWearable(pplayer.Clothes?.Sprite);
                    if (top != null)
                        inventory.Top = top;

                    Sprite bottom = _tileDatabase.GetWearable(pplayer.Shoes?.Sprite);
                    if (bottom != null)
                        inventory.Bottom = bottom;

                    Sprite accessory = _tileDatabase.GetWearable(pplayer.Accessory?.Sprite);
                    if (accessory != null)
                        inventory.Accessory = accessory;

                    Sprite weapon = _tileDatabase.GetWearable(character.Weapon?.Sprite);
                    if (weapon != null)
                        inventory.Weapon = weapon;
                }

            }

            foreach (var entry in state.MonsterNames)
            {
                PCharacter character = entry.Value.Character;
                PPosition position = character.Position;

                if (position.BoardId != _sceneConfiguration.BoardName)
                {
                    continue;
                }

                Sprite monsterSprite = _tileDatabase.GetSprite(character.Sprite);

                GameObject monster = this._charactersManager.CreateMonster(
                    new Vector3Int(position.X, position.Y, 0),
                    character.Name,
                    monsterSprite);

                var hub = monster.GetComponent<Hub>();

                hub.Health = character.CurrentHealth;
                hub.Experience = character.Experience;
                hub.Level = character.Level;
            }
        }
    }
}
