using UnityEngine;
using UnityEngine.Tilemaps;

namespace MM26.Board
{
    public class TreasureTrovesManager : MonoBehaviour
    {
        [SerializeField]
        private Tile _troveTile = null;

        [SerializeField]
        private Tilemap _tilemap = null;

        private bool[][] _troves = null;

        public void Initialize(int rows, int columns)
        {
            _troves = new bool[rows][];

            for (int y = 0; y < rows; y++)
            {
                var row = new bool[columns];

                _troves[y] = row;
            }
        }

        public void PlaceTrove(int x, int y)
        {
            Debug.Log("place trove");

            _tilemap.SetTile(new Vector3Int(x, y, -2), _troveTile);
            _troves[y][x] = true;
        }
    }
}
