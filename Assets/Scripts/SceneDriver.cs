using UnityEngine;

namespace MM26
{
    public enum SceneState
    {
        CreatingMap,
        CreatingTokens,
        Playing
    }

    public class SceneDriver : MonoBehaviour
    {
        [SerializeField]
        SceneLifeCycle _sceneLifeCycle = null;

        [SerializeField]
        DataProvider _dataProvider = null;

        [Header("Stages")]
        [SerializeField]
        bool _createMap = true;

        [SerializeField]
        bool _createTokens = true;

        [SerializeField]
        bool _play = true;

        private void OnEnable()
        {
            _dataProvider.CanBegin.AddListener(this.OnCanBegin);
        }

        private void OnDisable()
        {
            _dataProvider.CanBegin.RemoveListener(this.OnCanBegin);
        }

        private void Start()
        {
            _dataProvider.Start();
        }

        private void OnCanBegin()
        {
        }
    }
}