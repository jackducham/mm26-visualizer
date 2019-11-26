using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MM26.Map {
    [System.Serializable]
    public struct MapTile
    {
        // x-coordinate
        public int x;
        // y-coordinate
        public int y;
        // tile type (currently not in use)
        public int tileType;

        public MapTile(int x_, int y_, int tileType_)
        {
            x = x_;
            y = y_;
            tileType = tileType_;
        }

        /// <summary>
        /// Returns a string representation of the object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[Coords: ({0}, {1}), Type: {2}]", x, y, tileType);
        }

        public Vector3Int GetCoordinates()
        {
            return new Vector3Int(x, y, 0);
        }
    }
}