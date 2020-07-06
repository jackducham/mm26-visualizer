using UnityEngine;
using UnityEngine.Tilemaps;

namespace MM26.Board
{
    using PTileType = MM26.IO.Models.Tile.Types.TileType;
    using PTile = MM26.IO.Models.Tile;

    public class BoardCreator : MonoBehaviour
    {
        [SerializeField]
        private string _board = "pvp";

        [Header("Tiles")]
        [SerializeField]
        Tile _voidTile = null;

        [SerializeField]
        Tile _portalTile = null;

        [SerializeField]
        Tile _impassibleTile = null;

        [SerializeField]
        Tile _blankTile = null;

        [Header("Services")]
        [SerializeField]
        SceneLifeCycle  _sceneLifeCycle = null;

        [SerializeField]
        IO.Data _data = null;

        [Header("Others")]
        [SerializeField]
        Tilemap _tilemap = null;

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
            var board = _data.GameState.BoardNames[_board];

            // TODO: set tils here!
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

            _sceneLifeCycle.BoardCreated.Invoke();
        }
    }
}
