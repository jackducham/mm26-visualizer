using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using MM26.Components;

namespace MM26.Board
{
    public class CharactersManager : MonoBehaviour
    {
        public Tilemap Tilemap = null;

        [Header("Prefabs")]
        [SerializeField]
        private GameObject _playerPrefab = null;
        [SerializeField]
        private GameObject _monsterPrefab = null;

        public void CreateMonster(Vector3Int position, string name, Sprite sprite)
        {
            this.CreateCharacter(this._monsterPrefab, position, name, sprite);
        }

        public void CreatePlayer(Vector3Int position, string name)
        {
            this.CreateCharacter(this._playerPrefab, position, name);
        }

        /// <summary>
        /// Helper function for creating a player. This function assumes that
        /// the player is on the board we are currently creating
        /// </summary>
        /// <param name="prefab">the prefab to create character from</param>
        /// <param name="position">the position at which to creat a player</param>
        /// <param name="name">the name of the player</param>
        public void CreateCharacter(GameObject prefab, Vector3Int position, string name)
        {
            Vector3 wordPosition = this.Tilemap.GetCellCenterWorld(position);

            GameObject player = Instantiate(prefab, wordPosition, new Quaternion());

            // Initialize player
            player.name = name;

            Hub hub = player.GetComponent<Hub>();
            hub.NameLabel.text = name;
            hub.HealthLabel.text = "";
        }

        /// <summary>
        /// Helper function for creating a player. This function assumes that
        /// the player is on the board we are currently creating
        /// </summary>
        /// <param name="prefab">the prefab to create character from</param>
        /// <param name="position">the position at which to creat a player</param>
        /// <param name="name">the name of the player</param>
        /// <param name="sprite">the sprite to be used</param>
        public void CreateCharacter(GameObject prefab, Vector3Int position, string name, Sprite sprite)
        {
            Vector3 wordPosition = this.Tilemap.GetCellCenterWorld(position);

            GameObject player = Instantiate(prefab, wordPosition, new Quaternion());

            player.GetComponent<SpriteRenderer>().sprite = sprite;

            // Initialize player
            player.name = name;

            Hub hub = player.GetComponent<Hub>();
            hub.NameLabel.text = name;
            hub.HealthLabel.text = "";
        }
    }
}
