using System;
using UnityEngine;
using Google.Protobuf.Collections;
using MM26.ECS;
using MM26.IO.Models;
using MM26.Tasks;
using MM26.Board;

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
        private BoardPositionLookUp _positionLookUp = null;

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
            for (; _nextChangeIndex < _data.GameChanges.Count; _nextChangeIndex++)
            {
                GameChange gameChange = _data.GameChanges[_nextChangeIndex];
                TasksBatch batch = new TasksBatch();

                foreach (var pair in gameChange.CharacterStatChanges)
                {
                    string entity = pair.Key;
                    CharacterChange characterChange = pair.Value;

                    if (characterChange.Died)
                    {
                        batch.Add(new DespawnTask(entity));
                        continue;
                    }
                    else if (characterChange.Respawned)
                    {
                        throw new NotImplementedException("Respawned not handled in Director");
                    }

                    batch.Add(
                        new MovementTask(
                            entity,
                            this.GetPath(characterChange.Path)));
                }

                _taskManager.AddTasksBatch(batch);
            }
        }

        private Vector3[] GetPath(RepeatedField<Position> path)
        {
            Vector3[] newPath = new Vector3[path.Count];

            for (int i = 0; i < path.Count; i++)
            {
                Position position = path[i];
                newPath[i] = _positionLookUp.Translate(
                    new Vector3Int(position.X, position.Y, 0));
            }

            return newPath;
        }
    }
}
