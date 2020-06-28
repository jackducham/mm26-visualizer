using UnityEngine;
using UnityEngine.Tilemaps;
using MM26.IO;
using MM26.IO.Models;

namespace MM26.Board
{
    public class BoardCreator : MonoBehaviour
    {
        [SerializeField]
        private string _board = "pvp";

        [Header("Services")]
        [SerializeField]
        SceneLifeCycle  _sceneLifeCycle = null;

        [SerializeField]
        Data _data = null;

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
            Debug.Log("Create Board");
            IO.Models.Board board = _data.GameState.BoardNames[_board];
            Debug.Log(board);

            _sceneLifeCycle.BoardCreated.Invoke();
        }
    }
}
