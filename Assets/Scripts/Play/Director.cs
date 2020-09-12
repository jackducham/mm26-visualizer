using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;
using Google.Protobuf.Collections;
using MM26.ECS;
using MM26.IO;
using MM26.IO.Models;
using MM26.Tasks;
using MM26.Board;

[assembly:InternalsVisibleTo("MM26.Play.Tests")]

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

        internal void DispatchTasks()
        {
            while (Data.Turns.Count > 0)
            {
                VisualizerTurn turn = Data.Turns.Dequeue();
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
                    if (character.Position.BoardId != SceneConfiguration.BoardName)
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
                        batch.Add(new FollowPathTask(entity, path));
                    }
                }

                TaskManager.AddTasksBatch(batch);
            }
        }

        private Vector3[] GetPath(RepeatedField<Position> path)
        {
            Vector3[] newPath = new Vector3[path.Count];

            for (int i = 0; i < path.Count; i++)
            {
                Position position = path[i];
                newPath[i] = PositionLookUp.Translate(
                    new Vector3Int(position.X, position.Y, 0));
            }

            return newPath;
        }
    }
}
