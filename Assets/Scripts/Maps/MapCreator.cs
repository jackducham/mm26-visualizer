using UnityEngine;
using UnityEngine.Tilemaps;
using MM26.IO;
using MM26.IO.Models;

namespace MM26.Map
{
    public class MapCreator : MonoBehaviour
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
            _sceneLifeCycle.CreateMap.AddListener(this.OnCreateMap);
        }

        private void OnDisable()
        {
            _sceneLifeCycle.CreateMap.RemoveListener(this.OnCreateMap);
        }

        private void OnCreateMap()
        {
            Debug.Log("Create map");
            Board board = _data.GameState.BoardNames[_board];
            Debug.Log(board);

            _sceneLifeCycle.FinishCreatingMap();
        }
    }
}
