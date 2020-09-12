using System;
using UnityEngine;
using Google.Protobuf.Collections;
using MM26.ECS;
using MM26.IO;
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

        [SerializeField]
        private SceneConfiguration _sceneConfiguration = null;

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
            if (_data.Turns.Count > 0)
            {
                this.DispatchTasks();
            }
        }

        private void DispatchTasks()
        {
            while (_data.Turns.Count > 0)
            {
                VisualizerTurn turn = _data.Turns.Dequeue();
                GameChange gameChange = turn.Change;
                GameState gameState = turn.State;

                TasksBatch batch = new TasksBatch();

                foreach (var pair in gameChange.CharacterStatChanges)
                {
                    string entity = pair.Key;

                    IO.Models.Character character = null;

                    if (gameState.PlayerNames.ContainsKey(entity))
                    {
                        // if the entity is a player
                        character = gameState.PlayerNames[entity].Character;
                    }
                    else if (gameState.MonsterNames.ContainsKey(entity))
                    {
                        // if the entity is a monster
                        character = gameState.MonsterNames[entity].Character;
                    }
                    else
                    {
                        throw new Exception($"Don't know how to handle entity {entity}");
                    }

                    // skip entities not on this board
                    if (character.Position.BoardId != _sceneConfiguration.BoardName)
                    {
                        continue;
                    }

                    CharacterChange characterChange = pair.Value;

                    if (characterChange.Died)
                    {
                        batch.Add(new DespawnTask(entity));
                        continue;
                    }
                    else if (characterChange.Respawned)
                    {
                        batch.Add(new SpawnTask(entity, new Vector3Int(character.Position.X, character.Position.Y, 0)));
                    }
                    else if (characterChange.DecisionType == DecisionType.Portal)
                    {
                        batch.Add(new SpawnTask(entity, new Vector3Int(character.Position.X, character.Position.Y, 0)));
                    }
                    else
                    {
                        Vector3[] path = this.GetPath(characterChange.Path);
                        batch.Add(new MovementTask(entity, path));
                    }
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
