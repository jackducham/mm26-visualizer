using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MM26.Board.Helper {
    public class BoardCreatorHelper : MonoBehaviour
    {
        /// <summary>
        /// Trigger to generate Tile assets using .png files from g_BaseTilePath folder
        /// </summary>
        public bool run = false;
        public static string g_BaseTileObjPath = "Assets/Tiles/MM26_Tiles/mm_tile_objects/";
        public static string g_BaseTilePath = "Assets/Tiles/MM26_Tiles/mm_tiles/";


        private void Update()
        {
            if (run)
            {
                run = false;
                string[] dir = Directory.GetFiles(g_BaseTilePath + "collection/");

                foreach(string str in dir)
                {
                    if(!str.Contains(".meta"))
                        GenerateTileFullRelativePath(str);
                    //Debug.Log(str);

                }
            } 
        }

        public void GenerateTileFullRelativePath(string sourcePath)
        {
            Tile newTile = ScriptableObject.CreateInstance<Tile>();
            // Assuming sourcePath starts with "mm_tiles/" we remove it

            newTile.sprite = LoadExistingSprite(g_BaseTilePath + sourcePath.Replace("Assets/Tiles/MM26_Tiles/mm_tiles/", "")); //LoadNewSprite(sourcePath, 190.0f);
            //newTile.sprite = Resources.Load<Sprite>(g_BaseTileObjPath + sourcePath.Replace("Assets/Tiles/MM26_Tiles/mm_tiles/", ""));
            AssetDatabase.CreateAsset(newTile, g_BaseTileObjPath + sourcePath.Replace("Assets/Tiles/MM26_Tiles/mm_tiles/","").Replace(".png", ".asset").Replace(".PNG", ".asset"));
        }

        public void GenerateTile(string sourcePath)
        {
            Tile newTile = ScriptableObject.CreateInstance<Tile>();
            // Assuming sourcePath starts with "mm_tiles/" we remove it
            sourcePath = sourcePath.Remove(0, 9);

            newTile.sprite = LoadNewSprite(g_BaseTilePath + sourcePath, 190.0f);

            AssetDatabase.CreateAsset(newTile, g_BaseTileObjPath + sourcePath.Replace(".png", ".asset"));
        }
        
        public Sprite LoadExistingSprite(string FilePath)
        {
            return (Sprite)AssetDatabase.LoadAssetAtPath(FilePath, typeof(Sprite));
        }

        // TAKEN FROM UNITY FORUMS AT : https://forum.unity.com/threads/generating-sprites-dynamically-from-png-or-jpeg-files-in-c.343735/
        public Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 100.0f)
        {

            // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference

            Texture2D SpriteTexture = LoadTexture(FilePath);
            string targetPath = g_BaseTileObjPath + FilePath.Replace("Assets/Tiles/MM26_Tiles/mm_tiles/collection", "sprites");
            Sprite NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit);
            AssetDatabase.CreateAsset(NewSprite, targetPath);

            return NewSprite;
        }

        public Texture2D LoadTexture(string FilePath)
        {

            // Load a PNG or JPG file from disk to a Texture2D
            // Returns null if load fails

            Texture2D Tex2D;
            byte[] FileData;

            if (File.Exists(FilePath))
            {
                FileData = File.ReadAllBytes(FilePath);
                Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
                if (Tex2D.LoadImage(FileData))  // Load the imagedata into the texture (size is set automatically)
                {          

                    return Tex2D;                 // If data = readable -> return texture
                }
            } else
            {
                Debug.Log(FilePath);
            }
            return null;                     // Return null if load failed
        }
    }
}