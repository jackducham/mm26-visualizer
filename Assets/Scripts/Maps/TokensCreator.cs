using UnityEngine;

namespace MM26.Map
{
    public class TokensCreator : MonoBehaviour
    {
        [Header("Services")]
        [SerializeField]
        SceneLifeCycle _sceneLifeCycle = null;

        [SerializeField]
        Data _data = null;

        private void OnEnable()
        {
            _sceneLifeCycle.CreateTokens.AddListener(this.OnCreateTokens);
        }

        private void OnDisable()
        {
            _sceneLifeCycle.CreateTokens.RemoveListener(this.OnCreateTokens);
        }

        private void OnCreateTokens()
        {
            Debug.Log("Create Tokens");
            _sceneLifeCycle.FinishCreatingTokens();
        }
    }
}
