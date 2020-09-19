using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MM26.Board.Helper
{
    /// <summary>
    /// Stores list of tiles and can be used to fetch tiles by the path given from the protos
    /// Cannot work as a ScriptableObject, since Dictionaries aren't innately serializable
    /// </summary>
    public class TileDatabase : MonoBehaviour
    {
        // key: "mm_tiles/collection/" + tile png name
        public Dictionary<string, Tile> TileDictionary = new Dictionary<string, Tile>();



#if UNITY_EDITOR
        public void populateDictionary()
        {
            Debug.Log("Test populateDictionary()");
        }
#endif
    }
}