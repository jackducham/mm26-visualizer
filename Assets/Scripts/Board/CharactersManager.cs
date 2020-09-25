using UnityEngine;
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

        public GameObject CreateMonster(Vector3Int position, string name, Sprite sprite)
        {
            GameObject monster = this.CreateCharacter(this._monsterPrefab, position, name);
            monster.GetComponent<SpriteRenderer>().sprite = sprite;

            return monster;
        }

        public GameObject CreatePlayer(Vector3Int position, string name)
        {
            GameObject player = this.CreateCharacter(this._playerPrefab, position, name);
            var inventory = player.GetComponent<Inventory>();

            return player;
        }

        /// <summary>
        /// Helper function for creating a player. This function assumes that
        /// the player is on the board we are currently creating
        /// </summary>
        /// <param name="prefab">the prefab to create character from</param>
        /// <param name="position">the position at which to creat a player</param>
        /// <param name="name">the name of the player</param>
        private GameObject CreateCharacter(GameObject prefab, Vector3Int position, string name)
        {
            Vector3 wordPosition = this.Tilemap.GetCellCenterWorld(position);

            GameObject character = Instantiate(prefab, wordPosition, new Quaternion());

            // Initialize player
            character.name = name;

            var hub = character.GetComponent<Hub>();
            hub.NameLabel.text = name;
            hub.HealthLabel.text = "";

            return character;
        }
    }
}
