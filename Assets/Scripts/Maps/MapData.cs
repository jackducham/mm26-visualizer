using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MM26.Map
{
    [CreateAssetMenu(fileName = "New Map", menuName = "Map")]
    public class MapData : ScriptableObject
    {
        /// <summary>
        /// Name of the map
        /// </summary>
        public string MapName;

        /// <summary>
        /// Size of X dimension of the map
        /// </summary>
        public int Width;

        /// <summary>
        /// Size of Y dimension of the map
        /// </summary>
        public int Height;

        /// <summary>
        /// List of tile coordinates (z is tile type)
        /// </summary>
        public List<MapTile> Tiles;

        /// <summary>
        /// List of tile prefabs
        /// </summary>
        public List<Tile> TilePrefabs;

        public void Init(string name, int w, int h, List<MapTile> tiles)
        {
            MapName = name;
            Width = w;
            Height = h;
            Tiles = tiles;
        }

        public void Init(string name, int w, int h, List<MapTile> tiles, List<Tile> tileList)
        {
            MapName = name;
            Width = w;
            Height = h;
            Tiles = tiles;
            TilePrefabs = tileList;
        }

        public static MapData CreateInstance(string name, int w, int h, List<MapTile> tiles)
        {
            MapData board = CreateInstance<MapData>();
            board.Init(name, w, h, tiles);
            return board;
        }

        public static MapData CreateInstance(string name, int w, int h, List<MapTile> tiles, List<Tile> tileList)
        {
            MapData board = CreateInstance<MapData>();
            board.Init(name, w, h, tiles, tileList);
            return board;
        }
    }
}
