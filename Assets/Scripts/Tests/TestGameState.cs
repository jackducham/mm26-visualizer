using System;

namespace MM26.Tests
{
    /// <summary>
    /// Mock board
    /// </summary>
    [Serializable]
    public class TestBoard
    {
        /// <summary>
        /// Name of the map
        /// </summary>
        public string Name = "pvp";

        /// <summary>
        /// Size of X dimension of the map
        /// </summary>
        public int Columns;

        /// <summary>
        /// Size of Y dimension of the map
        /// </summary>
        public int Rows;

        /// <summary>
        /// List of tile coordinates (z is tile type)
        /// </summary>
        public TestTile[] Grid;
    }

    [Serializable]
    public class TestGameState
    {
        public TestBoard Board = null;
        public TestCharacter[] Characters = null;
    }
}