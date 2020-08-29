using System;
using UnityEngine;

namespace MM26.Board.Tests
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

    /// <summary>
    /// Mock game state
    /// </summary>
    [CreateAssetMenu(fileName = "New Test Board", menuName = "Board/Test Board")]
    public class TestGameState : ScriptableObject
    {
        public TestBoard Board = null;
        public TestPlayer[] Players = null;
    }
}
