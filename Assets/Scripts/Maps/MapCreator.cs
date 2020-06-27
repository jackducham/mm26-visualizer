using UnityEngine;

namespace MM26.Map
{
    public class MapCreator : MonoBehaviour
    {
        [Header("Services")]
        [SerializeField]
        SceneLifeCycleService _sceneLifeCycleService = null;

        [SerializeField]
        DataService _dataService = null;

        private void OnEnable()
        {
            _sceneLifeCycleService.CreateMap.AddListener(this.OnCreateMap);
        }

        private void OnDisable()
        {
            _sceneLifeCycleService.CreateMap.RemoveListener(this.OnCreateMap);
        }

        private void OnCreateMap()
        {
            Debug.Log("Create map");
        }
    }
}
