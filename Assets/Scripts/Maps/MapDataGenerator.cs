using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.Tilemaps;

namespace MM26.Map
{
    /// <summary>
    /// This class is intended to be used in the editor.
    /// It should NOT be used in runtime, ever!
    /// </summary>
    public class MapDataGenerator : MonoBehaviour
    {
        public enum FileType
        {
            TXT, JSON
        }

        [Header("Important GameObjects")]
        public Tilemap tilemap;

        [Header("Tile Prefabs")]
        public Tile[] tilePrefabs;

        [Header("Map Object")]
        // Assets/Resources/Maps/[MapObjectPath]
        public string mapName;
        public FileType fileType;

        private string mapsFolderPath;
        private string mapFileType
        {
            get
            {
                if (fileType == FileType.TXT)
                {
                    return ".txt";
                }
                else if (fileType == FileType.JSON)
                {
                    return ".json";
                }
                else
                {
                    return ".inval";
                }
            }
        }

        private string mapPath
        {
            get
            {
                return mapsFolderPath + mapName + mapFileType;
            }
        }


        /// <summary>
        /// Reads map from a specific type of input
        /// </summary>
        /// <param name="type">0: file, 1: tilemap</param>
        public void ReadMapFrom(int type)
        {
            mapsFolderPath = Application.dataPath + "/Resources/Maps/" + mapName + "/";
            Debug.Log("Look at map path: " + mapPath);
            if (!File.Exists(mapPath))
            {
                Debug.Log("Map does not exist " + mapPath);
                return;
            }
            string newAssetPath = "Assets/Resources/Maps/" + mapName + "/" + mapName + ".asset";

            if (!File.Exists(newAssetPath))
            {
                Debug.Log("Map found!");
                string[] lines = File.ReadAllLines(@"" + mapPath);

                MapData board;
                if (type == 0)
                    board = ParseMapFromText(lines);
                else //if (type == 1)
                    board = ParseMapFromTilemap();

                AssetDatabase.CreateAsset(board, newAssetPath);
            }
            else
            {
                Debug.Log("File already exists");
            }
        }

        private MapData ParseMapFromTilemap()
        {
            int width = tilemap.size.x;
            int height = tilemap.size.y;
            List<MapTile> Tiles = new List<MapTile>();

            Dictionary<Tile, int> tileDict = new Dictionary<Tile, int>();
            List<Tile> tileList = new List<Tile>();

            int tileCount = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Tile curTile = (Tile)tilemap.GetTile(new Vector3Int(x, y, 0));

                    if (curTile != null)
                    {
                        if (!tileDict.ContainsKey(curTile))
                        {
                            tileDict.Add(curTile, tileCount);
                            tileList.Add(curTile);
                            tileCount++;
                        }

                        int tileIdx = tileDict[curTile];

                        Tiles.Add(new MapTile(x, y, tileIdx));
                    }
                }
            }

            MapData board = MapData.CreateInstance(mapName, width, height, Tiles, tileList);

            return board;
        }

        private MapData ParseMapFromText(string[] text)
        {
            List<MapTile> Tiles = new List<MapTile>();

            string[] dim = text[0].Split(',');
            int width = int.Parse(dim[0]);
            int height = int.Parse(dim[1]);


            for (int i = 1; i <= height; i++)
            {
                int y = i - 1;
                string[] cells = text[i].Split(',');
                for (int j = 0; j < width; j++)
                {
                    int x = j;
                    int cellData;
                    if ((cellData = int.Parse(cells[j])) != 0)
                    {
                        MapTile curTile = new MapTile(x, y, cellData - 1);
                        Tiles.Add(curTile);
                    }
                }
            }

            List<Tile> defaultTiles = new List<Tile>();

            foreach (Tile t in tilePrefabs)
            {
                defaultTiles.Add(t);
            }

            MapData board = MapData.CreateInstance(mapName, width, height, Tiles, defaultTiles);

            return board;
        }
    }
}