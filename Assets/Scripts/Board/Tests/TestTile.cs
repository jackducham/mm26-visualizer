using System;
using MM26.IO.Models;

namespace MM26.Board.Tests
{
    [Serializable]
    public enum TestTileType
    {
        Void,
        Blank,
        Impassible,
        Portal
    }

    [Serializable]
    public struct TestTile
    {

        /// <summary>
        /// tile type (currently not in use)
        /// </summary>
        public Tile.Types.TileType TileType;
    }
}
