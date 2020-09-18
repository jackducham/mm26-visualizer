﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Google.Protobuf.Collections;
using MM26.IO.Models;
using MM26.ECS;
using MM26.Tasks;
using MM26.Board;

[assembly: InternalsVisibleTo("MM26.Play.Tests")]

namespace MM26.Play
{
    internal static class VisualizerTurnExtensions
    {
        /// <summary>
        /// Create a task batch from a turn
        /// </summary>
        /// <param name="turn">the turn</param>
        /// <param name="sceneConfiguration">
        /// configuration of this scene
        /// </param>
        /// <param name="boardPositionLookUp">lookup service</param>
        /// <returns></returns>
        internal static TasksBatch ToTasksBatch(
            this VisualizerTurn turn,
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
                CharacterChange characterChange = pair.Value;


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

                ProcessCharacter(batch, entity, character, characterChange, sceneConfiguration, boardPositionLookUp, ignoreForHubUpdate);
            }

            ProcessGameState(batch, sceneConfiguration, gameState, ignoreForHubUpdate);

            return batch;
        }

        private static void ProcessCharacter(
            TasksBatch batch,
            string entity,
            Character character,
            CharacterChange characterChange,
            SceneConfiguration sceneConfiguration,
            BoardPositionLookUp boardPositionLookUp,
            HashSet<string> ignoreForHubUpdate)
        {
            if (characterChange.Died)
            {
                if (character.Position.BoardId == sceneConfiguration.BoardName)
                {
                    ignoreForHubUpdate.Add(entity);
                    batch.Add(new DespawnTask(entity));
                }
                
                return;
            }
            else if (characterChange.Respawned)
            {
                if (character.Position.BoardId == sceneConfiguration.BoardName)
                {
                    ignoreForHubUpdate.Add(entity);

                    var position = new Vector3Int(
                        character.Position.X,
                        character.Position.Y,
                        0);

                    batch.Add(new SpawnPlayerTask(entity, position));
                }
                
                return;
            }

            switch (characterChange.Decision.DecisionType)
            {
                case DecisionType.Move:
                    if (character.Position.BoardId != sceneConfiguration.BoardName)
                    {
                        return;
                    }

                    Vector3[] path = GetPath(characterChange.Path, boardPositionLookUp);
                    batch.Add(new FollowPathTask(entity, path));
                    break;
                case DecisionType.Portal:
                    ignoreForHubUpdate.Add(entity);

                    if (character.Position.BoardId != sceneConfiguration.BoardName)
                    {
                        batch.Add(new DespawnTask(entity));
                    }
                    else
                    {
                        var position = new Vector3Int(
                            character.Position.X,
                            character.Position.Y,
                            0);

                        batch.Add(new SpawnPlayerTask(entity, position));
                    }
                    break;
                case DecisionType.None:
                    break;
                default:
                    Debug.LogWarningFormat("Unrecognized decision type {0}", characterChange.Decision.DecisionType);
                    break;
            }
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
                    Health = player.Character.CurrentHealth,
                    Level = player.Character.Level,
                    Experience = player.Character.Experience
                };

                taskBatch.Add(task);
            }

            foreach (var pair in gameState.MonsterNames)
            {
                string entity = pair.Key;
                Monster monster = pair.Value;

                if (monster.Character.Position.BoardId != sceneConfiguration.BoardName)
                {
                    continue;
                }

                if (ignoreForHubUpdate.Contains(entity))
                {
                    continue;
                }

                var task = new UpdateHubTask(entity)
                {
                    Health = monster.Character.CurrentHealth,
                    Level = monster.Character.Level,
                    Experience = monster.Character.Experience
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