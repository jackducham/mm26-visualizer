using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace MM26
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
        private TextMeshProUGUI _url;

        [SerializeField]
        private TextMeshProUGUI _boardName;

        public void OnLoadClick()
        {
            _sceneConfiguration.WebSocketURL = _url.text;
            _sceneConfiguration.BoardName = _boardName.text;
            SceneManager.LoadScene(_sceneName);
        }
    }
}
