using UnityEngine;
using MM26.ECS;
using MM26.IO.Models;

namespace MM26.Play
{
    /// <summary>
    /// Responsible for playing the scene from deltas
    /// </summary>
    public class Director : MonoBehaviour
    {
        [Header("Scene Essentials")]
        [SerializeField]
        private SceneLifeCycle _sceneLifeCycle = null;

        [SerializeField]
        private MM26.IO.Data _data = null;

        [SerializeField]
        private TasksManager _taskManager = null;

        private int _nextChangeIndex = 0;

        private void OnEnable()
        {
            _sceneLifeCycle.Play.AddListener(this.DispatchTasks);
        }

        private void OnDisable()
        {
            _sceneLifeCycle.Play.RemoveListener(this.DispatchTasks);
        }

        private void Update()
        {
            if (_nextChangeIndex < _data.GameChanges.Count)
            {
                this.DispatchTasks();
            }
        }

        private void DispatchTasks()
        {
            for (int i = 0; i < _data.GameChanges.Count; i++)
            {
                GameChange change = _data.GameChanges[i];

                Debug.Log(change.NewPlayerNames.Count);
            }
        }
    }
}
