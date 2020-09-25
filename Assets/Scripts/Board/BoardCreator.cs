using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Tilemaps;
using MM26.Components;
using MM26.ECS;
using MM26.Tasks;
using MM26.Configuration;
using MM26.Board.Helper;

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


        private void OnEnable()
        {
            _sceneLifeCycle.CreateBoard.AddListener(this.OnCreateMap);
        }

        private void OnDisable()
        {
            _sceneLifeCycle.CreateBoard.RemoveListener(this.OnCreateMap);
        }

        /// <summary>
        /// Event handler for <c>_sceneLifeCycle.CreateBoard</c>
        /// </summary>
        private void OnCreateMap()
        {
            if (_data.Initial.State.BoardNames.ContainsKey(_sceneConfiguration.BoardName))
            {
                this.CreateMap();
                this.CreateCharacters();
            }
        }

        /// <summary>
        /// Helper function for creating the board
        /// </summary>
        private void CreateMap()
        {
            var board = _data.Initial.State.BoardNames[_sceneConfiguration.BoardName];

            _positionLookUp.Grid = _grid;
            _positionLookUp.Height = board.Height;
            _positionLookUp.Width = board.Width;

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
        private void CreateCharacters()
        {
            foreach (var entry in _data.Initial.State.PlayerNames)
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
            }

            foreach (var entry in _data.Initial.State.MonsterNames)
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
