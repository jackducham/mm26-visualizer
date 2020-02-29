using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MM26.Map
{
    public class TilemapGenerator : MonoBehaviour
    {
        [Header("Important GameObjects")]
        public Tilemap tilemap;

        [Header("Map Data")]
        public MapData board;

        [Header("Set z to 0 if not in use, 1 otherwise")]
        public Vector3Int mapSize;

        public void FillTilemap()
        {
            if (mapSize.z == 0)
                SetCellBounds();
            else
                SetCellBoundsByEditor();

            for (int i = 0; i < board.Tiles.Count; i++)
            {
                tilemap.SetTile(board.Tiles[i].GetCoordinates(), board.TilePrefabs[board.Tiles[i].tileType]);
            }
        }

        public void ClearTilemap()
        {
            tilemap.ClearAllTiles();
        }

        private void SetCellBounds()
        {
            int width, height;
            width = board.Width;
            height = board.Height;
            tilemap.size = new Vector3Int(width, height, 1);
            tilemap.origin = new Vector3Int();
            tilemap.ResizeBounds();
        }

        public void SetCellBoundsByEditor()
        {
            tilemap.size = mapSize;
            tilemap.origin = new Vector3Int();
            tilemap.ResizeBounds();
        }
    }
}