using UnityEngine;
using UnityEngine.SceneManagement;

namespace MM26
{
    public class Bootstrap : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        private string _mainScene = "Main";

        [SerializeField]
        private WebsocketDataProvider _websocketDataProvider = null;

        private AsyncOperation _loadingOperation = null;

        private void Start()
        {
            _websocketDataProvider.Connect();
        }

        private void OnEnable()
        {
            _websocketDataProvider.Connected.AddListener(this.OnConnected);
        }

        private void OnDisable()
        {
            _websocketDataProvider.Connected.RemoveListener(this.OnConnected);
        }

        private void OnConnected()
        {
            _loadingOperation = SceneManager.LoadSceneAsync(_mainScene);
        }
    }
}
