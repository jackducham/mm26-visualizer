using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MM26.Board.Helper
{
    /// <summary>
    /// This is not actually meant to be used in the build. Only use it to generate Tile assets
    /// </summary>
    public class BoardCreatorHelper : MonoBehaviour
    {
        /// <summary>
        /// Trigger to generate Tile assets using .png files from g_BaseTilePath folder
        /// </summary>
        public bool run = false;
        public static string g_BaseTileObjPath = "Assets/Sprites/mm26_tiles/";
        public static string g_BaseTilePath = "Assets/Sprites/mm26_tiles/raw/";


        private void Update()
        {
            if (run)
            {
                run = false;
                string[] dir = Directory.GetFiles(g_BaseTilePath);

                foreach (string str in dir)
                {
                    if (!str.Contains(".meta"))
                        GenerateTile(str);

                }
            }
        }

        /// <summary>
        /// Generates a tile object from a Sprite
        /// </summary>
        /// <param name="sourcePath"></param>
        public void GenerateTile(string sourcePath)
        {
            Tile newTile = ScriptableObject.CreateInstance<Tile>();
            // Assuming sourcePath starts with "mm_tiles/" we remove it

            newTile.sprite = LoadExistingSprite(sourcePath); //LoadNewSprite(sourcePath, 190.0f);
            Debug.Log(sourcePath.Replace(g_BaseTilePath, g_BaseTileObjPath).Replace(".png", ".asset").Replace(".PNG", ".asset"));
            //newTile.sprite = Resources.Load<Sprite>(g_BaseTileObjPath + sourcePath.Replace("Assets/Tiles/MM26_Tiles/mm_tiles/", ""));
            AssetDatabase.CreateAsset(newTile, sourcePath.Replace(g_BaseTilePath, g_BaseTileObjPath).Replace(".png", ".asset").Replace(".PNG", ".asset"));
        }

        /// <summary>
        /// Fetches a Sprite object from given the file path
        /// </summary>
        /// <param name="sourcePath">The relative path to the object starting with "Assets/"</param>
        /// <returns></returns>
        public Sprite LoadExistingSprite(string sourcePath)
        {
            return (Sprite)AssetDatabase.LoadAssetAtPath(sourcePath, typeof(Sprite));
        }

    }
}