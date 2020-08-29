﻿using System;
using MM26.IO.Models;

namespace MM26.Board.Tests
{
    /// <summary>
    /// Mock tile type
    /// </summary>
    [Serializable]
    public enum TestTileType
    {
        Void,
        Blank,
        Impassible,
        Portal
    }

    /// <summary>
    /// Mock tile
    /// </summary>
    [Serializable]
    public struct TestTile
    {

        /// <summary>
        /// tile type (currently not in use)
        /// </summary>
        public Tile.Types.TileType TileType;
    }
}
