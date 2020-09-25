using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

[assembly : InternalsVisibleTo("Assembly-CSharp-Editor")]

namespace MM26.Configuration
{
    /// <summary>
    /// Stores a lookup table between path and sprite objects
    /// </summary>
    [CreateAssetMenu(fileName = "SpriteLookUp", menuName = "Configuration/Sprite Look Up")]
    public class SpriteLookUp : ScriptableObject
    {
        [Serializable]
        internal struct TileEntry
        {
            public string Path;
            public Tile Tile;
        }

        [Serializable]
        internal struct WearableEntry
        {
            public string Path;
            public Sprite Sprite;
            public string Slot; // bottom, top, head_sprite
        }

        [SerializeField]
        internal TileEntry[] TileEntries = null;

        [SerializeField]
        internal WearableEntry[] WearableEntries = null;

        [SerializeField]
        private Tile _fallbackTile = null;

        [Header("Generate Settings")]
        public string TilesPath = "Assets/Sprites/mm26_tiles/";
        public string WearablesPath = "Assets/Sprites/mm26_wearables/";

        Dictionary<string, Tile> _tiles = null;
        Dictionary<string, Sprite> _sprites = null;
        Dictionary<string, string> _spriteSlots = null;

        private void OnEnable()
        {
            this.InitializeDictionaries();
        }

        public void InitializeDictionaries()
        {
            _tiles = new Dictionary<string, Tile>();
            _sprites = new Dictionary<string, Sprite>();
            _spriteSlots = new Dictionary<string, string>();

            foreach (TileEntry entry in this.TileEntries)
            {
                _tiles[entry.Path] = entry.Tile;
            }

            foreach (WearableEntry entry in this.WearableEntries)
            {
                _sprites[entry.Path] = entry.Sprite;
                _spriteSlots[entry.Path] = entry.Slot;
            }
        }

        public Tile GetTile(string path)
        {
            // some path might have all cap extensions, but the path
            // found by SpriteLookUpEditor all have lower case extension
            string directory = Path.GetDirectoryName(path);
            string filename = Path.GetFileNameWithoutExtension(path);
            string extension = Path.GetExtension(path).ToLower();

            path = directory + "/" + $"{filename}{extension}";

            if (_tiles.TryGetValue(path, out Tile tile))
            {
                return tile;
            }
#if UNITY_EDITOR
            // Please preserve this comment
            Debug.LogWarningFormat("Tile at {0} is not found!", path);
#endif

            return _fallbackTile;
        }

        public Sprite GetSprite(string path)
        {
            Tile t = GetTile(path);
            return t.sprite;
        }
    }
}
