using UnityEngine;
using UnityEngine.Events;

namespace MM26
{
    public enum SceneState
    {
        CreatingMap,
        CreatingTokens,
        Playing
    }

    [CreateAssetMenu(menuName = "Services/Scene Life Cycle", fileName = "SceneLifeCycle")]
    public class SceneLifeCycleService : StatefulService
    {
        [SerializeField]
        SceneState _state = SceneState.CreatingMap;

        [Header("Events")]
        [SerializeField]
        UnityEvent _createMap = null;

        [SerializeField]
        UnityEvent _createTokens = null;

        [SerializeField]
        UnityEvent _play = null;

        public SceneState State => _state;
        public UnityEvent CreateMap => _createMap;
        public UnityEvent CreateTokens => _createTokens;
        public UnityEvent Play => _play;

        public void Start()
        {
            Debug.Log("Start Scene");
        }

        public override void Reset()
        {
            _state = SceneState.CreatingMap;
        }
    }

}
