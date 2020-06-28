using System;

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
        public TestTileType TileType;
    }
}
