using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MM26.Board
{
    [CreateAssetMenu(fileName = "BoardPositionLookUp", menuName = "Board/Board Position Lookup")]
    public class BoardPositionLookUp : ScriptableObject
    {
        public Tilemap Tilemap { get; set; } = null;

        public Vector3 Translate(Vector3Int position)
        {
            return this.Tilemap.GetCellCenterWorld(position);
        }

        public void Reset()
        {
            this.Tilemap = null;
        }
    }
}