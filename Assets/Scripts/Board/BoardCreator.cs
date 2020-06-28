using UnityEngine;
using UnityEngine.Tilemaps;

namespace MM26.Board
{
    using PBoard = MM26.IO.Models.Board;

    public class BoardCreator : MonoBehaviour
    {
        [SerializeField]
        private string _board = "pvp";

        [Header("Tiles")]
        [SerializeField]
        Tile _tile = null;

        [Header("Services")]
        [SerializeField]
        SceneLifeCycle  _sceneLifeCycle = null;

        [SerializeField]
        IO.Data _data = null;

        [Header("Others")]
        [SerializeField]
        Tilemap _tilemap = null;

        private void OnEnable()
        {
            _sceneLifeCycle.CreateBoard.AddListener(this.OnCreateMap);
        }

        private void OnDisable()
        {
            _sceneLifeCycle.CreateBoard.RemoveListener(this.OnCreateMap);
        }

        private void OnCreateMap()
        {
            var board = _data.GameState.BoardNames[_board];

            // TODO: set tils here!
            _tilemap.SetTile(new Vector3Int(), _tile);

            _sceneLifeCycle.BoardCreated.Invoke();
        }
    }
}
