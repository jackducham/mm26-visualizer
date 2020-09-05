using UnityEngine;
using UnityEngine.Tilemaps;
using MM26.Components;

namespace MM26.Board
{
    using PTileType = MM26.IO.Models.Tile.Types.TileType;
    using PTile = MM26.IO.Models.Tile;
    using PCharacter = MM26.IO.Models.Character;
    using PPosition = MM26.IO.Models.Position;

    /// <summary>
    /// Responsible for creating the board
    /// </summary>
    public class BoardCreator : MonoBehaviour
    {
        [Header("Tiles")]
        [SerializeField]
        private Tile _voidTile = null;

        [SerializeField]
        private Tile _portalTile = null;

        [SerializeField]
        private Tile _impassibleTile = null;

        [SerializeField]
        private Tile _blankTile = null;

        [Header("Players")]
        [SerializeField]
        private GameObject _playerPrefab = null;

        [Header("Scene Specific")]
        [SerializeField]
        private SceneConfiguration _sceneConfiguration = null;

        [Header("Scene Essentials")]
        [SerializeField]
        private SceneLifeCycle  _sceneLifeCycle = null;

        [SerializeField]
        private IO.Data _data = null;

        [SerializeField]
        private BoardPositionLookUp _positionLookUp = null;

        [Header("Others")]
        [SerializeField]
        private Tilemap _tilemap = null;

        private void OnEnable()
        {
            _sceneLifeCycle.CreateBoard.AddListener(this.OnCreateMap);
        }

        private void OnDisable()
        {
            _sceneLifeCycle.CreateBoard.RemoveListener(this.OnCreateMap);
        }

        private void OnCreateMap()
        {
            this.CreateMap();
            this.CreatePlayers();

            _positionLookUp.Tilemap = _tilemap;

            _sceneLifeCycle.BoardCreated.Invoke();
        }

        private void CreateMap()
        {
            var board = _data.GameState.BoardNames[_sceneConfiguration.BoardName];

            for (int y = 0; y < board.Rows; y++)
            {
                for (int x = 0; x < board.Columns; x++)
                {
                    PTile ptile = board.Grid[y * board.Rows + x];
                    Tile tile = null;

                    switch (ptile.TileType)
                    {
                        case PTileType.Void:
                            tile = _voidTile;
                            break;
                        case PTileType.Portal:
                            tile = _portalTile;
                            break;
                        case PTileType.Impassible:
                            tile = _impassibleTile;
                            break;
                        case PTileType.Blank:
                            tile = _blankTile;
                            break;
                    }

                    if (tile == null)
                    {
                        Debug.LogError("tile cannot be null!");
                        continue;
                    }

                    _tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }
        }

        private void CreatePlayers()
        {
            foreach (var entry in _data.GameState.PlayerNames)
            {
                PCharacter playerCharacter = entry.Value.Character;
                PPosition position = playerCharacter.Position;

                if (position.BoardId != _sceneConfiguration.BoardName)
                {
                    continue;
                }

                Vector3 wordPosition = _tilemap.GetCellCenterWorld(new Vector3Int(position.X, position.Y, 0));
                GameObject player = Instantiate(_playerPrefab, wordPosition, new Quaternion());

                // Initialize player
                player.name = playerCharacter.Name;
                player.GetComponent<IDComponent>().Name = playerCharacter.Name;
            }
        }
    }
}
