using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using MM26.Utilities;

namespace MM26.UI
{
    /// <summary>
    /// Serves as the loader for the main scene
    /// </summary>
    public class MainLoader : MonoBehaviour
    {
        [SerializeField]
        private SceneConfiguration _sceneConfiguration = null;

        [SerializeField]
        private string _sceneName = "Main";

        [SerializeField]
        TMP_InputField _boardNameField = null;

        [SerializeField]
        TextMeshProUGUI _serverLabel = null;

        string _url = "ws://engine-main.mechmania.io:8081/visualizer";

        private void Start()
        {
            _boardNameField.text = "pvp";

            _sceneConfiguration.WebSocketURL = "ws://engine-main.mechmania.io:8081/visualizer";
            _sceneConfiguration.BoardName = "pvp";
        }

        public void OnUseMainServer()
        {
            _serverLabel.text = "Server: Main";
            _url = "ws://engine-main.mechmania.io:8081/visualizer";
        }

        public void OnUseTestServer()
        {
            _serverLabel.text = "Server: Test";
            _url = "ws://engine-test.mechmania.io:8081/visualizer";
        }

        public void OnUseLocalHost()
        {
            _serverLabel.text = "Server: Local";
            _url = "ws://localhost:8081/visualizer";
        }

        public void OnLoadClick()
        {
            _sceneConfiguration.WebSocketURL = _url;
            _sceneConfiguration.BoardName = _boardNameField.text.KeepVisibles();

            SceneManager.LoadScene(_sceneName);
        }
    }
}
