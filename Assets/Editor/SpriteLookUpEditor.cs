using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using MM26.Configuration;

[CustomEditor(typeof(SpriteLookUp))]
public class SpriteLookUpEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SpriteLookUp spriteLookUp = (SpriteLookUp)this.target;

        if (GUILayout.Button("Generate"))
        {
            this.PopulateDatabase();
            spriteLookUp.InitializeDictionaries();
        }
    }

    /// <summary>
    /// Populates Dictionary and TileList with Tiles
    /// </summary>
    public void PopulateDatabase()
    {
        SpriteLookUp spriteLookUp = (SpriteLookUp)this.target;

        spriteLookUp.TileEntries = Directory.GetFiles(spriteLookUp.TilesPath)
            .Where(asset => !asset.Contains(".DS_Store"))
            .Where(asset => Path.GetExtension(asset) != ".meta")
            .Select(asset =>
            {
                string path = asset.Replace(spriteLookUp.TilesPath, "mm26_tiles/").Replace(".asset", ".png");
                Tile tile = (Tile)AssetDatabase.LoadAssetAtPath(asset, typeof(Tile));

                if (tile == null)
                {
                    Debug.LogErrorFormat("asset not found at {0}", path);
                }

                // some path might have all cap extensions, but the path
                // found by SpriteLookUpEditor all have lower case extension
                string directory = Path.GetDirectoryName(path);
                string name = Path.GetFileNameWithoutExtension(path);
                string extension = Path.GetExtension(path);

                return new SpriteLookUp.TileEntry()
                {
                    Path = directory + "/" + $"{name}{extension}",
                    Tile = tile,
                };
            })
            .ToArray();


        spriteLookUp.WearableEntries = Directory.GetFiles(spriteLookUp.WearablesPath, "*.*", SearchOption.AllDirectories)
            .Where(asset => !asset.Contains(".DS_Store"))
            .Where(asset => Path.GetExtension(asset) != ".meta")
            .Select(asset =>
            {
                string path = asset.Replace(spriteLookUp.WearablesPath, "mm26_wearables/"); //.Replace(".PNG", ".png");
                Sprite sprite = (Sprite)AssetDatabase.LoadAssetAtPath(asset, typeof(Sprite));

                if (sprite == null)
                {
                    Debug.LogErrorFormat("asset not found at {0}", path);
                }

                // some path might have all cap extensions, but the path
                // found by SpriteLookUpEditor all have lower case extension
                string directory = Path.GetDirectoryName(path).Replace("\\", "/"); ;
                string name = Path.GetFileNameWithoutExtension(path);
                string extension = Path.GetExtension(path);

                string[] dir_slot = directory.Split('/');

                string slot = "unassigned";

                if (dir_slot.Length > 1)
                    slot = dir_slot[1];
                
                
                return new SpriteLookUp.WearableEntry()
                {
                    Path = directory + "/" + $"{name}{extension}",
                    Sprite = sprite,
                    Slot = slot
                };
            })
            .ToArray();
    }
}
