using UnityEngine;
using UnityEngine.Tilemaps;

namespace MM26.Board
{
    public class TreasureTrovesManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _trovePrefab = null;

        [SerializeField]
        private Tilemap _tilemap = null;

        private GameObject[][] _troves = null;

        public void Initialize(int rows, int columns)
        {
            _troves = new GameObject[rows][];

            for (int y = 0; y < rows; y++)
            {
                var row = new GameObject[columns];

                _troves[y] = row;
            }
        }

        public void PlaceTrove(int x, int y)
        {
            GameObject trove = Instantiate(_trovePrefab);

            //Vector3 position = _tilemap.GetCellCenterWorld(position);
        }
    }
}
