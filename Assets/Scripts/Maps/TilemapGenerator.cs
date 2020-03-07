using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Serialization;

namespace MM26.Map
{
    public class TilemapGenerator : MonoBehaviour
    {
        [Header("Important GameObjects")]
        [FormerlySerializedAs("tilemap")]
        public Tilemap Tilemap;

        [Header("Map Data")]
        [FormerlySerializedAs("board")]
        public MapData Board;

        [Header("Set z to 0 if not in use, 1 otherwise")]
        [FormerlySerializedAs("mapSize")]
        public Vector3Int MapSize;

        public void FillTilemap()
        {
            if (MapSize.z == 0)
                SetCellBounds();
            else
                SetCellBoundsByEditor();

            for (int i = 0; i < Board.Tiles.Count; i++)
            {
                Tilemap.SetTile(Board.Tiles[i].GetCoordinates(), Board.TilePrefabs[Board.Tiles[i].TileType]);
            }
        }

        public void ClearTilemap()
        {
            Tilemap.ClearAllTiles();
        }

        private void SetCellBounds()
        {
            int width, height;
            width = Board.Width;
            height = Board.Height;
            Tilemap.size = new Vector3Int(width, height, 1);
            Tilemap.origin = new Vector3Int();
            Tilemap.ResizeBounds();
        }

        public void SetCellBoundsByEditor()
        {
            Tilemap.size = MapSize;
            Tilemap.origin = new Vector3Int();
            Tilemap.ResizeBounds();
        }
    }
}
