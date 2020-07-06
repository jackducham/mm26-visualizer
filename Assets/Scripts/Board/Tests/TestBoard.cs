using UnityEngine;

namespace MM26.Board.Tests
{
    [CreateAssetMenu(fileName = "New Test Board", menuName = "Board/Test Board")]
    public class TestBoard : ScriptableObject
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
}
