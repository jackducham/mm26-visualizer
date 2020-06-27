using UnityEngine;

namespace MM26
{
    public class SceneDriver : MonoBehaviour
    {
        [SerializeField]
        SceneLifeCycleService _sceneLifeCycleService = null;

        private void Start()
        {
            _sceneLifeCycleService.Start();
        }
    }
}