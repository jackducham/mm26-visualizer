using System;
using System.Collections.Generic;
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

        private void DispatchTasks()
        {
            while (Data.Turns.Count > 0)
            {
                this.TaskManager.AddTasksBatch(
                    GetTasksBatch(
                        Data.Turns.Dequeue(),
                        this.SceneConfiguration,
                        this.PositionLookUp));
            }
        }

        /// <summary>
        /// Create a task batch from a turn
        /// </summary>
        /// <param name="turn">the turn</param>
        /// <param name="sceneConfiguration">
        /// configuration of this scene
        /// </param>
        /// <param name="boardPositionLookUp">lookup service</param>
        /// <returns></returns>
        internal static TasksBatch GetTasksBatch(
            VisualizerTurn turn,
            SceneConfiguration sceneConfiguration,
            BoardPositionLookUp boardPositionLookUp)
        {
            GameChange gameChange = turn.Change;
            GameState gameState = turn.State;

            TasksBatch batch = new TasksBatch();
            var ignoreForHubUpdate = new HashSet<string>();
            

            foreach (var pair in gameChange.CharacterChanges)
            {
                string entity = pair.Key;
                Character character = null;


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
                if (character.Position.BoardId != sceneConfiguration.BoardName)
                {
                    continue;
                }

                CharacterChange characterChange = pair.Value;

                if (characterChange.Died)
                {
                    ignoreForHubUpdate.Add(entity);
                    batch.Add(new DespawnTask(entity));
                    continue;
                }
                else if (characterChange.Respawned)
                {
                    ignoreForHubUpdate.Add(entity);
                    batch.Add(
                        new SpawnPlayerTask(
                            entity,
                            new Vector3Int(
                                character.Position.X,
                                character.Position.Y,
                                0)));
                }

                switch (characterChange.Decision.DecisionType)
                {
                    case DecisionType.Move:
                        Vector3[] path = Director.GetPath(characterChange.Path, boardPositionLookUp);
                        batch.Add(new FollowPathTask(entity, path));
                        break;
                    case DecisionType.Portal:
                        ignoreForHubUpdate.Add(entity);
                        batch.Add(
                            new SpawnPlayerTask(
                                entity,
                                new Vector3Int(
                                    character.Position.X,
                                    character.Position.Y,
                                    0)));
                        break;
                    case DecisionType.None:
                        break;
                    default:
                        Debug.LogWarningFormat("Unrecognized decision type {0}", characterChange.Decision.DecisionType);
                        break;
                }
            }

            ProcessGameState(batch, sceneConfiguration, gameState, ignoreForHubUpdate);

            return batch;
        }

        /// <summary>
        /// Process state
        /// </summary>
        /// <param name="taskBatch">the task batch</param>
        /// <param name="sceneConfiguration">
        /// the configuration of this scene
        /// </param>
        /// <param name="gameState">the current game state</param>
        private static void ProcessGameState(
            TasksBatch taskBatch,
            SceneConfiguration sceneConfiguration,
            GameState gameState,
            HashSet<string> ignoreForHubUpdate)
        {
            foreach (var pair in gameState.PlayerNames)
            {
                string entity = pair.Key;
                Player player = pair.Value;

                if (player.Character.Position.BoardId != sceneConfiguration.BoardName)
                {
                    continue;
                }

                if (ignoreForHubUpdate.Contains(entity))
                {
                    continue;
                }

                var task = new UpdateHubTask(entity)
                {
                    Health = player.Character.CurrentHealth
                };

                taskBatch.Add(task);
            }
        }

        private static Vector3[] GetPath(RepeatedField<Position> path, BoardPositionLookUp boardPositionLookUp)
        {
            Vector3[] newPath = new Vector3[path.Count];

            for (int i = 0; i < path.Count; i++)
            {
                Position position = path[i];
                newPath[i] = boardPositionLookUp.Translate(
                    new Vector3Int(position.X, position.Y, 0));
            }

            return newPath;
        }
    }
}
