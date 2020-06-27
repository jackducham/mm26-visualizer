using UnityEngine;
using UnityEngine.Events;

namespace MM26
{
    [CreateAssetMenu(menuName = "Services/Scene Life Cycle", fileName = "SceneLifeCycle")]
    public class SceneLifeCycle : ScriptableObject
    {
        [Header("Events")]
        [SerializeField]
        UnityEvent _createMap = null;

        [SerializeField]
        UnityEvent _mapCreated = null;

        [SerializeField]
        UnityEvent _createTokens = null;

        [SerializeField]
        UnityEvent _tokensCreated = null;

        [SerializeField]
        UnityEvent _play = null;

        public UnityEvent CreateMap => _createMap;
        public UnityEvent MapCreated => _mapCreated;

        public UnityEvent CreateTokens => _createTokens;
        public UnityEvent TokensCreated => _tokensCreated;

        public UnityEvent Play => _play;

        public void StartCreatingMap()
        {
            Debug.Log("Start Scene");
        }

        public void StartCreatingTokens()
        {

        }

        public void StartPlaying()
        {

        }

        public void FinishCreatingMap()
        {
            _mapCreated.Invoke();
        }

        public void FinishCreatingTokens()
        {
            _tokensCreated.Invoke();
        }
    }
}
