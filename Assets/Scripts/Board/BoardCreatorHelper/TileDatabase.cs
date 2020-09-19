﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using ICSharpCode.NRefactory.Ast;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
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

        public static TileDatabase Instance;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            } else
            {
                this.gameObject.SetActive(false);
            }
        }

        public Tile GetTile(string tilePath)
        {
            // If the path is 
            if(!TileDictionary.ContainsKey(tilePath))
            {
                Debug.LogErrorFormat("Tile at {0} not found in Dictionary. Might need to regenerate tile list", tilePath);
                return null;
            }

            return TileDictionary[tilePath];
        }


#if UNITY_EDITOR
        [SerializeField]
        private List<Tile> TileList = new List<Tile>();

        public static string g_BaseTileObjPath = "Assets/Tiles/MM26_Tiles/mm_tile_objects/";
        public static string g_BaseTilesFolderPath = "Assets/Tiles/MM26_Tiles/";

        /// <summary>
        /// Populates Dictionary and TileList with Tiles
        /// </summary>
        public void PopulateDictionary()
        {
            Debug.Log("Running populateDictionary()");

            TileDictionary = new Dictionary<string, Tile>();
            TileList = new List<Tile>();

            string[] dir = Directory.GetFiles(g_BaseTileObjPath + "collection/");
            int count = 0;
            foreach (string asset in dir)
            {
                if (!asset.Contains(".meta"))
                {
                    count++;
                    string key = asset.Replace(g_BaseTileObjPath, "mm_tiles/").Replace(".asset", ".png");
                    Tile val = (Tile)AssetDatabase.LoadAssetAtPath(asset, typeof(Tile));
                    //Debug.Log(key);
                    TileDictionary.Add(key, val);
                    TileList.Add(val);
                }
            }

            Debug.LogFormat("Added {0} tiles", count);

            TestDatabase();
        }

        /// <summary>
        /// Checks if every tile that should be present is present in the dictionary
        /// </summary>
        public void TestDatabase()
        {
            Debug.Log("Running populateDictionary()");

            string[] dir = Directory.GetFiles(g_BaseTileObjPath + "collection/");
            int count = 0;
            foreach (string asset in dir)
            {
                if (!asset.Contains(".meta"))
                {
                    string key = asset.Replace(g_BaseTileObjPath, "mm_tiles/").Replace(".asset", ".png");
                    Debug.LogFormat("Searching for key {0}", key);
                    if(TileDictionary.ContainsKey(key))
                    {
                        Debug.LogFormat("Found key {0}", key);
                        count++;

                        Debug.LogFormat("Has tile {0}", TileDictionary[key].name);
                    } else
                    {
                        Debug.LogErrorFormat("Did not find key {0}", key);
                    }
                }
            }

            Debug.LogFormat("Found {0} tiles", count);
        }
#endif
    }
}