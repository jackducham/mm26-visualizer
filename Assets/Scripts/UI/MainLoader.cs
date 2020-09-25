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
        TMP_InputField _urlField = null;

        [SerializeField]
        TMP_InputField _boardNameField = null;

        private void Start()
        {
            _urlField.text = _sceneConfiguration.WebSocketURL;
            _boardNameField.text = _sceneConfiguration.BoardName;
        }

        public void OnLoadClick()
        {
            _sceneConfiguration.WebSocketURL = _urlField.text.KeepVisibles();
            _sceneConfiguration.BoardName = _boardNameField.text.KeepVisibles();

            SceneManager.LoadScene(_sceneName);
        }
    }
}
