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
        }

        [SerializeField]
        internal TileEntry[] TileEntries = null;

        [SerializeField]
        internal WearableEntry[] WearableEntries = null;

        [SerializeField]
        private Tile _fallbackTile = null;

        [Header("Generate Settings")]
        public string TilesPath = "Assets/Sprites/mm26_tiles/";

        Dictionary<string, Tile> _tiles = null;

        private void OnEnable()
        {
            this.InitializeDictionaries();
        }

        public void InitializeDictionaries()
        {
            _tiles = new Dictionary<string, Tile>();

            foreach (TileEntry entry in this.TileEntries)
            {
                _tiles[entry.Path] = entry.Tile;
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
