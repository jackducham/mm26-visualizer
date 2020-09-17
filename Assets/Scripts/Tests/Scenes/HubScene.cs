using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MM26.ECS;
using MM26.Tasks;

namespace MM26.Tests.Scenes
{
    public class HubScene : MonoBehaviour
    {
        [SerializeField]
        TasksManager _taskManager = null;

        private void Start()
        {
            StartCoroutine(this.Simulate());
        }

        private IEnumerator Simulate()
        {
            TasksBatch batchA = new TasksBatch()
            {
                new UpdateHubTask("Test Player")
                {
                    Health = 100
                }
            };

            _taskManager.AddTasksBatch(batchA);

            yield return new WaitForSecondsRealtime(2.0f);

            TasksBatch batchB = new TasksBatch()
            {
                new UpdateHubTask("Test Player")
                {
                    Health = 50,
                    Level = 50,
                    Experience = 50
                }
            };

            _taskManager.AddTasksBatch(batchB);
        }
    }
}
