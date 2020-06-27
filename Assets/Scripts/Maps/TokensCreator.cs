using UnityEngine;

namespace MM26.Map
{
    public class TokensCreator : MonoBehaviour
    {
        [Header("Services")]
        [SerializeField]
        SceneLifeCycleService _sceneLifeCycleService = null;

        [SerializeField]
        DataService _dataService = null;

        private void OnEnable()
        {
            _sceneLifeCycleService.CreateTokens.AddListener(this.OnCreateTokens);
        }

        private void OnDisable()
        {
            _sceneLifeCycleService.CreateTokens.RemoveListener(this.OnCreateTokens);
        }

        private void OnCreateTokens()
        {

        }
    }
}
