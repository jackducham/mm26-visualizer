using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using MM26.Components;
using MM26.ECS;
using MM26.Tasks;

namespace MM26.Board
{
    using PTileType = MM26.IO.Models.Tile.Types.TileType;
    using PTile = MM26.IO.Models.Tile;
    using PCharacter = MM26.IO.Models.Character;
    using PPosition = MM26.IO.Models.Position;

    /// <summary>
    /// Responsible for creating and managing the board
    /// </summary>
    public sealed class BoardCreator : MonoBehaviour
    {
        [Header("Tiles")]
        [SerializeField]
        private Tile _voidTile = null;

        [SerializeField]
        private Tile _portalTile = null;

        [SerializeField]
        private Tile _impassibleTile = null;

        [SerializeField]
        private Tile _blankTile = null;

        [Header("Players")]
        [SerializeField]
        private GameObject _playerPrefab = null;

        [Header("Scene Specific")]
        [SerializeField]
        private SceneConfiguration _sceneConfiguration = null;

        [Header("Scene Essentials")]
        [SerializeField]
        private SceneLifeCycle  _sceneLifeCycle = null;

        [SerializeField]
        private IO.Data _data = null;

        [SerializeField]
        private BoardPositionLookUp _positionLookUp = null;

        [Header("ECS")]
        [SerializeField]
        private Mailbox _mailbox = null;

        [Header("Others")]
        [SerializeField]
        private Tilemap _tilemap = null;

        private void OnEnable()
        {
            _sceneLifeCycle.CreateBoard.AddListener(this.OnCreateMap);
            _mailbox.SubscribeToTaskType<SpawnTask>(this);
        }

        private void OnDisable()
        {
            _sceneLifeCycle.CreateBoard.RemoveListener(this.OnCreateMap);
        }

        private void Update()
        {
            this.HandleSpawnTasks();
            this.HandleDespawnTasks();
        }

        /// <summary>
        /// Event handler for <c>_sceneLifeCycle.CreateBoard</c>
        /// </summary>
        private void OnCreateMap()
        {
            this.CreateMap();
            this.CreatePlayers();

            _positionLookUp.Tilemap = _tilemap;

            _sceneLifeCycle.BoardCreated.Invoke();
        }

        /// <summary>
        /// Helper function for creating the board
        /// </summary>
        private void CreateMap()
        {
            var board = _data.GameState.BoardNames[_sceneConfiguration.BoardName];

            for (int y = 0; y < board.Rows; y++)
            {
                for (int x = 0; x < board.Columns; x++)
                {
                    PTile ptile = board.Grid[y * board.Rows + x];
                    Tile tile = null;

                    switch (ptile.TileType)
                    {
                        case PTileType.Void:
                            tile = _voidTile;
                            break;
                        case PTileType.Portal:
                            tile = _portalTile;
                            break;
                        case PTileType.Impassible:
                            tile = _impassibleTile;
                            break;
                        case PTileType.Blank:
                            tile = _blankTile;
                            break;
                    }

                    if (tile == null)
                    {
                        Debug.LogError("tile cannot be null!");
                        continue;
                    }

                    _tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }
        }

        /// <summary>
        /// Helper function for creating players
        /// </summary>
        private void CreatePlayers()
        {
            foreach (var entry in _data.GameState.PlayerNames)
            {
                PCharacter playerCharacter = entry.Value.Character;
                PPosition position = playerCharacter.Position;

                if (position.BoardId != _sceneConfiguration.BoardName)
                {
                    continue;
                }

                this.CreatePlayer(new Vector3Int(position.X, position.Y, 0), playerCharacter.Name);
            }
        }

        /// <summary>
        /// Helper function for creating a player
        /// </summary>
        /// <param name="position">the position at which to creat a player</param>
        /// <param name="name">the name of the player</param>
        private void CreatePlayer(Vector3Int position, string name)
        {
            Vector3 wordPosition = _tilemap.GetCellCenterWorld(position);

            GameObject player = Instantiate(_playerPrefab, wordPosition, new Quaternion());

            // Initialize player
            player.name = name;
            player.GetComponent<ID>().Name = name;
        }

        /// <summary>
        /// Helper function to handle spawn tasks
        ///
        /// 
        /// </summary>
        private void HandleSpawnTasks()
        {
            List<Task> tasks = _mailbox.GetSubscribedTasksForType<SpawnTask>(this);

            for (int i = 0; i < tasks.Count; i++)
            {
                SpawnTask task = tasks[i] as SpawnTask;

                this.CreatePlayer(task.Position, task.EntityName);

                task.IsFinished = true;
                _mailbox.RemoveTask(task);
            }
        }

        /// <summary>
        /// Helper function to handle despawn tasks
        /// 
        /// <b>Noe that this is called once per frame</b>
        /// </summary>
        private void HandleDespawnTasks()
        {
            List<Task> tasks = _mailbox.GetSubscribedTasksForType<DespawnTask>(this);

            for (int i = 0; i < tasks.Count; i++)
            {
                DespawnTask task = tasks[i] as DespawnTask;

                // FIXME: might cause performance issue (this is on a hot path)
                GameObject entity = GameObject.Find(task.EntityName);
                GameObject.Destroy(entity);

                task.IsFinished = true;
                _mailbox.RemoveTask(task);
            }
        }
    }
}
