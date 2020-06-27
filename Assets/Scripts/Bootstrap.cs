using UnityEngine;
using UnityEngine.SceneManagement;

namespace MM26
{
    public class Bootstrap : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        private string _map = "pvp";

        [SerializeField]
        private string _mainScene = "Main";

        [Header("Services")]
        [SerializeField]
        private ResetService _resetService = null;

        [SerializeField]
        private DataService _dataService = null;

        private AsyncOperation _loadingOperation = null;

        private void Start()
        {
            _resetService.Reset();
            _dataService.Connect();
        }

        private void OnEnable()
        {
            _dataService.Connected.AddListener(this.OnConnected);
        }

        private void OnDisable()
        {
            _dataService.Connected.RemoveListener(this.OnConnected);
        }

        private void OnConnected()
        {
            _loadingOperation = SceneManager.LoadSceneAsync(_mainScene);
        }
    }
}
