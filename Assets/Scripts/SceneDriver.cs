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

        [SerializeField]
        Resets _resetsOnExit = null;

        [Header("Stages")]
        [SerializeField]
        bool _createMap = true;

        [SerializeField]
        bool _createTokens = true;

        [SerializeField]
        bool _play = true;

        private void OnEnable()
        {
            _dataProvider.CanStart.AddListener(this.OnCanStart);
            _sceneLifeCycle.MapCreated.AddListener(this.OnMapCreated);
            _sceneLifeCycle.TokensCreated.AddListener(this.OnTokensCreated);
        }

        private void OnDisable()
        {
            _dataProvider.CanStart.RemoveListener(this.OnCanStart);
            _sceneLifeCycle.MapCreated.RemoveListener(this.OnMapCreated);
            _sceneLifeCycle.TokensCreated.RemoveListener(this.OnTokensCreated);
        }

        private void Start()
        {
            _resetsOnExit.Reset();
            _dataProvider.Start();
        }

        private void OnCanStart()
        {
            if (_createMap)
            {
                _sceneLifeCycle.StartCreatingMap();
            }
            else if (_createTokens)
            {
                _sceneLifeCycle.StartCreatingTokens();
            }
            else if (_play)
            {
                _sceneLifeCycle.StartPlaying();
            }
        }

        private void OnMapCreated()
        {
            if (_createTokens)
            {
                _sceneLifeCycle.StartCreatingTokens();
            }
            else if (_play)
            {
                _sceneLifeCycle.StartPlaying();
            }
        }

        private void OnTokensCreated()
        {
            if (_play)
            {
                _sceneLifeCycle.StartPlaying();
            }
        }
    }
}