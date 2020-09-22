using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
            return _tiles[path];
        }
    }
}
