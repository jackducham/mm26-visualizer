using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.Tilemaps;
using UnityEngine.Serialization;

namespace MM26.Map
{
    public enum FileType
    {
        TXT, JSON
    }

    /// <summary>
    /// This class is intended to be used in the editor.
    /// It should NOT be used in runtime, ever!
    /// </summary>
    public class MapDataGenerator : MonoBehaviour
    {
        [Header("Important GameObjects")]
        [FormerlySerializedAs("tilemap")]
        public Tilemap Tilemap;

        [Header("Tile Prefabs")]
        [FormerlySerializedAs("tilemap")]
        public Tile[] TilePrefabs;

        [Header("Map Object")]
        [FormerlySerializedAs("mapName")]
        public string MapName;

        [FormerlySerializedAs("fileType")]
        public FileType FileType;

        private string _mapsFolderPath;
        private string _mapFileType
        {
            get
            {
                if (FileType == FileType.TXT)
                {
                    return ".txt";
                }
                else if (FileType == FileType.JSON)
                {
                    return ".json";
                }
                else
                {
                    return ".inval";
                }
            }
        }

        private string _mapPath
        {
            get
            {
                return _mapsFolderPath + MapName + _mapFileType;
            }
        }


        /// <summary>
        /// Reads map from a specific type of input
        /// </summary>
        /// <param name="type">0: file, 1: tilemap</param>
        public void ReadMapFrom(int type)
        {
            _mapsFolderPath = Application.dataPath + "/Resources/Maps/" + MapName + "/";
            Debug.Log("Look at map path: " + _mapPath);
            if (!File.Exists(_mapPath))
            {
                Debug.Log("Map does not exist " + _mapPath);
                return;
            }
            string newAssetPath = "Assets/Resources/Maps/" + MapName + "/" + MapName + ".asset";

            if (!File.Exists(newAssetPath))
            {
                Debug.Log("Map found!");
                string[] lines = File.ReadAllLines(@"" + _mapPath);

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
            int width = Tilemap.size.x;
            int height = Tilemap.size.y;
            List<MapTile> Tiles = new List<MapTile>();

            Dictionary<Tile, int> tileDict = new Dictionary<Tile, int>();
            List<Tile> tileList = new List<Tile>();

            int tileCount = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Tile curTile = (Tile)Tilemap.GetTile(new Vector3Int(x, y, 0));

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

            MapData board = MapData.CreateInstance(MapName, width, height, Tiles, tileList);

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

            foreach (Tile t in TilePrefabs)
            {
                defaultTiles.Add(t);
            }

            MapData board = MapData.CreateInstance(MapName, width, height, Tiles, defaultTiles);

            return board;
        }
    }
}
