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

        public int mapHeight = 0;

        public void PlaceTrove(int x, int y)
        {
            Debug.Log(_tilemap.cellSize);
            Debug.Log(_tilemap.name);
            _tilemap.SetTile(new Vector3Int(x, (mapHeight - 1) - y, 0), _troveTile);
        }

        public void RemoveTrove(int x, int y)
        {
            _tilemap.SetTile(new Vector3Int(x, (mapHeight - 1) - y, 0), null);
        }
    }
}
