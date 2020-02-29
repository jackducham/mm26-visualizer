using UnityEngine;
using UnityEngine.Serialization;

namespace MM26.Map
{
    [System.Serializable]
    public struct MapTile
    {
        // x-coordinate
        [FormerlySerializedAs("x")]
        public int X;

        // y-coordinate
        [FormerlySerializedAs("y")]
        public int Y;
        // tile type (currently not in use)
        [FormerlySerializedAs("tileType")]
        public int TileType;

        public MapTile(int x_, int y_, int tileType_)
        {
            X = x_;
            Y = y_;
            TileType = tileType_;
        }

        /// <summary>
        /// Returns a string representation of the object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[Coords: ({0}, {1}), Type: {2}]", X, Y, TileType);
        }

        public Vector3Int GetCoordinates()
        {
            return new Vector3Int(X, Y, 0);
        }
    }
}
