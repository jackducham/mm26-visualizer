using UnityEngine;
using MM26.ECS;
using MM26.Tasks;
using MM26.Components;

namespace MM26.Scenes.Behaviors
{
    public class MovementSceneController : MonoBehaviour
    {
        [SerializeField]
        Transform[] _patrolPoints = null;

        [SerializeField]
        TasksManager _tasksManager = null;

        [SerializeField]
        IdComponent _character;

        private void Start()
        {
            for (int i = 0; i < _patrolPoints.Length; i++)
            {
                Transform patrol = _patrolPoints[i];

                _tasksManager.AddTasksBatch(new TasksBatch()
                {
                    new MovementTask(_character.ID, patrol)
                });
            }
        }
    }
}