using UnityEngine;
using UnityEngine.Events;

namespace MM26
{
    [CreateAssetMenu(menuName = "Scene Life Cycle", fileName = "SceneLifeCycle")]
    public class SceneLifeCycle : ScriptableObject
    {
        [Header("Events")]
        [SerializeField]
        UnityEvent _fetchData = null;

        [SerializeField]
        UnityEvent _dataFetched = null;

        [SerializeField]
        UnityEvent _createBoard = null;

        [SerializeField]
        UnityEvent _boardCreated = null;

        [SerializeField]
        UnityEvent _play = null;

        [SerializeField]
        UnityEvent _reset = null;

        public UnityEvent FetchData => _fetchData;
        public UnityEvent DataFetched => _dataFetched;

        public UnityEvent CreateBoard => _createBoard;
        public UnityEvent BoardCreated => _boardCreated;

        public UnityEvent Play => _play;
        public UnityEvent Reset => _reset;
    }
}
