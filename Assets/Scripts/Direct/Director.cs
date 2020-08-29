using UnityEngine;

namespace MM26.Play
{
    /// <summary>
    /// Responsible for playing the scene from deltas
    /// </summary>
    public class Director : MonoBehaviour
    {
        [Header("Services")]
        [SerializeField]
        private SceneLifeCycle _sceneLifeCycle = null;

        [SerializeField]
        private MM26.IO.Data _data = null;

        private void OnEnable()
        {
            _sceneLifeCycle.Play.AddListener(this.Play);
        }

        private void OnDisable()
        {
            _sceneLifeCycle.Play.RemoveListener(this.Play);
        }

        private void Play()
        {
            
        }
    }
}
