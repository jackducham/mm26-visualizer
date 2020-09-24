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
        private Tilemap _tilemap = null;

        [SerializeField]
        internal CharactersManager CharactersManager = null;


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
            this.CreateMap();
            this.CreateCharacters();

            _positionLookUp.Tilemap = _tilemap;

            _sceneLifeCycle.BoardCreated.Invoke();
        }

        /// <summary>
        /// Helper function for creating the board
        /// </summary>
        private void CreateMap()
        {
            var board = _data.Initial.State.BoardNames[_sceneConfiguration.BoardName];


            for (int y = 0; y < board.Rows; y++)
            {
                for (int x = 0; x < board.Columns; x++)
                {
                    PTile ptile = board.Grid[y * board.Rows + x];

                    if (ptile.TileType == PTileType.Void)
                    {
                        continue;
                    }

                    if (ptile.GroundSprite != "")
                    {
                        Tile tile = _tileDatabase.GetTile(ptile.GroundSprite);
                        _tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                    }

                    if (ptile.AboveSprite != "")
                    {
                        Tile tile = _tileDatabase.GetTile(ptile.AboveSprite);
                        _tilemap.SetTile(new Vector3Int(x, y, -1), tile);
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
                PCharacter character = entry.Value.Character;
                PPosition position = character.Position;

                if (position.BoardId != _sceneConfiguration.BoardName)
                {
                    continue;
                }

                this.CharactersManager.CreatePlayer(new Vector3Int(position.X, position.Y, 0), character.Name);
            }

            foreach (var entry in _data.Initial.State.MonsterNames)
            {
                PCharacter character = entry.Value.Character;
                PPosition position = character.Position;

                if (position.BoardId != _sceneConfiguration.BoardName)
                {
                    continue;
                }
                Debug.Log(character.Sprite);
                Sprite monsterSprite = _tileDatabase.GetSprite(character.Sprite);
               
                this.CharactersManager.CreateMonster(new Vector3Int(position.X, position.Y, 0), character.Name, monsterSprite);
            }
        }
    }
}
