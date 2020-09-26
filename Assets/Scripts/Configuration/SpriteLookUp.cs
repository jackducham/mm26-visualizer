using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

[assembly: InternalsVisibleTo("Assembly-CSharp-Editor")]

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
        Dictionary<string, Sprite> _wearables = null;
        Dictionary<string, string> _wearableSlots = null;

        private void OnEnable()
        {
            this.InitializeDictionaries();
        }

        public void InitializeDictionaries()
        {
            _tiles = new Dictionary<string, Tile>();
            _wearables = new Dictionary<string, Sprite>();
            _wearableSlots = new Dictionary<string, string>();

            foreach (TileEntry entry in this.TileEntries)
            {
                _tiles[entry.Path] = entry.Tile;
            }

            foreach (WearableEntry entry in this.WearableEntries)
            {
                _wearables[entry.Path] = entry.Sprite;
                _wearableSlots[entry.Path] = entry.Slot;
            }
        }

        public Sprite GetWearable(string path)
        {
            // some path might have all cap extensions, but the path
            // found by SpriteLookUpEditor all have lower case extension
            //string directory = Path.GetDirectoryName(path);
            //string filename = Path.GetFileNameWithoutExtension(path);
            //string extension = Path.GetExtension(path).ToLower();

            if (path == "" || path == null)
                return null;

            //path = (directory + "/" + $"{filename}{extension}").Replace('\\', '/');
            path = path.Replace("\\", "/").Replace(".PNG", ".png");
            if (_wearables.TryGetValue(path, out Sprite sprite))
            {
                return sprite;
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarningFormat("Failed to get sprite from path {0}", path);
#endif
                return null;
            }
        }

        /// <summary>
        /// Returns the slot id of the equipment at a given path
        /// </summary>
        /// <param name="path">path to the item</param>
        /// <returns>the slot id</returns>
        public string GetWearableSlot(string path)
        {
            // some path might have all cap extensions, but the path
            // found by SpriteLookUpEditor all have lower case extension
            string directory = Path.GetDirectoryName(path);
            string filename = Path.GetFileNameWithoutExtension(path);
            string extension = Path.GetExtension(path).ToLower();

            path = directory + "/" + $"{filename}{extension}";

            if (_wearableSlots.TryGetValue(path, out string slot))
            {
                return slot;
            }
            else
            {
                return "";
            }
        }

        public Tile GetTile(string path)
        {
            try
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
            catch (Exception)
            {
                return _fallbackTile;
            }
        }

        public Sprite GetSprite(string path)
        {
            Tile t = GetTile(path);
            return t.sprite;
        }
    }
}
