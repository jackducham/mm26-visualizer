using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace MM26.Map
{
    /// <summary>
    /// This class is intended to be used in the editor.
    /// It should NOT be used in runtime, ever!
    /// </summary>
    public class MapGenerator : MonoBehaviour
    {
        public enum FileType
        {
            TXT, JSON
        }
        [Header("Important GameObjects")]
        public GameObject mapParent;

        [Header("Map Object")]
        // Assets/Resources/Maps/[MapObjectPath]
        public string mapName;
        public FileType fileType;

        [Header("Tile Prefabs")]
        public GameObject[] tiles;

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

        public void ReadMapFrom()
        {
            mapsFolderPath = Application.dataPath + "/Resources/Maps/";
            Debug.Log("Look at map path: " + mapPath);
            if (!File.Exists(mapPath))
            {
                Debug.Log("Map does not exist " + mapPath);
                return;
            }

            Debug.Log("Map found!");
            string[] lines = File.ReadAllLines(@"" + mapPath);

            for (int i = 0; i < lines.Length; i++)
            {
                Debug.Log(lines[i]);
            }
        }
    }
}