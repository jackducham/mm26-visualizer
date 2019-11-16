using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MM26.Map
{
    [CreateAssetMenu(fileName = "New Map", menuName = "Map")]
    public class MapData : ScriptableObject
    {
        // Name of the map
        public string MapName;
        // Size of X dimension of the map
        public int Width;
        // Size of Y dimension of the map
        public int Height;
        // List of tile coordinates
        List<MapTile> Tiles;
    }
}