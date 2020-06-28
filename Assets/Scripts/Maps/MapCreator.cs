using UnityEngine;
using MM26.IO;

namespace MM26.Map
{
    public class MapCreator : MonoBehaviour
    {
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
            Debug.Log("Create Maps");
            _sceneLifeCycle.FinishCreatingMap();
        }
    }
}
