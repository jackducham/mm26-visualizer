﻿using UnityEngine;
using UnityEngine.Tilemaps;

namespace MM26.Board
{
    public class TreasureTrovesManager : MonoBehaviour
    {
        [SerializeField]
        private Tile _troveTile = null;

        [SerializeField]
        private Tilemap _tilemap = null;

        public void PlaceTrove(int x, int y)
        {
            _tilemap.SetTile(new Vector3Int(x, y, 0), _troveTile);
        }

        public void RemoveTrove(int x, int y)
        {
            _tilemap.SetTile(new Vector3Int(x, y, 0), null);
        }
    }
}
