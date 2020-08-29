using UnityEngine;
using UnityEngine.Tilemaps;

namespace MM26.Board
{
    /// <summary>
    /// Allows other components to perform position lookup in the tile map
    /// </summary>
    [CreateAssetMenu(fileName = "BoardPositionLookUp", menuName = "Board/Board Position Lookup")]
    public class BoardPositionLookUp : ScriptableObject
    {
        public Tilemap Tilemap { get; set; } = null;

        /// <summary>
        /// Given a tilemap position, translate to game position
        /// </summary>
        /// <param name="position">tilemap position</param>
        /// <returns>game position</returns>
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