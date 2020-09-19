using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;
using MM26.ECS;
using MM26.IO;
using MM26.Board;

[assembly: InternalsVisibleTo("MM26.Play.Tests")]

namespace MM26.Play
{
    /// <summary>
    /// Responsible for playing the scene from deltas
    /// </summary>
    public class Director : MonoBehaviour
    {
        [Header("Scene Essentials")]
        [SerializeField]
        [FormerlySerializedAs("_sceneLifeCycle")]
        internal SceneLifeCycle SceneLifeCycle = null;

        [SerializeField]
        [FormerlySerializedAs("_data")]
        internal MM26.IO.Data Data = null;

        [SerializeField]
        [FormerlySerializedAs("_positionLookUp")]
        internal BoardPositionLookUp PositionLookUp = null;

        [SerializeField]
        [FormerlySerializedAs("_taskManager")]
        internal TasksManager TaskManager = null;

        [SerializeField]
        [FormerlySerializedAs("_sceneConfiguration")]
        internal SceneConfiguration SceneConfiguration = null;

        private void OnEnable()
        {
            SceneLifeCycle.Play.AddListener(this.DispatchTasks);
        }

        private void OnDisable()
        {
            SceneLifeCycle.Play.RemoveListener(this.DispatchTasks);
        }

        private void Update()
        {
            this.DispatchTasks();
        }

        private void DispatchTasks()
        {
            while (Data.Turns.Count > 0)
            {
                this.TaskManager.AddTasksBatch(
                    Data.Turns.Dequeue().ToTasksBatch(this.SceneConfiguration, this.PositionLookUp));
            }
        }
    }
}
