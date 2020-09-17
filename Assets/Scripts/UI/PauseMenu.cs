using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace MM26.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas = null;

        [SerializeField]
        private string _introScene= "Intro";

        private Configuration.Input _input = null;

        private void OnEnable()
        {
            _input = new Configuration.Input();
            _input.Enable();

            _input.Camera.Pause.performed += this.OnPause;
        }

        private void OnDisable()
        {
            _input.Dispose();
        }

        private void Start()
        {
            _canvas.enabled = false;
        }

        private void OnPause(InputAction.CallbackContext context)
        {
            _canvas.enabled = !_canvas.enabled;
        }

        public void OnExitBoardClick()
        {
            SceneManager.LoadScene(_introScene);
        }

        public void OnExitVisualizerClick()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
