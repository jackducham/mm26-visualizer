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

<<<<<<< HEAD
        public int mapHeight = 0;

        public void PlaceTrove(int x, int y)
        {
            Debug.Log(_tilemap.cellSize);
            Debug.Log(_tilemap.name);
            _tilemap.SetTile(new Vector3Int(x, (mapHeight - 1) - y, 0), _troveTile);
=======
        [SerializeField]
        private BoardPositionLookUp _boardPositionLookUp = null;

        public void PlaceTrove(int x, int y)
        {
            _tilemap.SetTile(new Vector3Int(x, (_boardPositionLookUp.Height - 1) - y, 0), _troveTile);
>>>>>>> 42f5e6b1eec72c193dc6959baaec1893579e88a8
        }

        public void RemoveTrove(int x, int y)
        {
<<<<<<< HEAD
            _tilemap.SetTile(new Vector3Int(x, (mapHeight - 1) - y, 0), null);
=======
            _tilemap.SetTile(new Vector3Int(x, (_boardPositionLookUp.Height - 1) - y, 0), null);
>>>>>>> 42f5e6b1eec72c193dc6959baaec1893579e88a8
        }
    }
}
