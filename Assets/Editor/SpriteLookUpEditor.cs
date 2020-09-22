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

    public static string g_BaseTileObjPath = "Assets/Tiles/MM26_Tiles/mm_tile_objects/";
    public static string g_BaseTilesFolderPath = "Assets/Tiles/MM26_Tiles/";

    /// <summary>
    /// Populates Dictionary and TileList with Tiles
    /// </summary>
    public void PopulateDatabase()
    {
        SpriteLookUp spriteLookUp = (SpriteLookUp)this.target;

        spriteLookUp.TileEntries = Directory.GetFiles(Path.Combine(g_BaseTileObjPath, "collection"))
            .Where(asset => Path.GetExtension(asset) != ".meta")
            .Select(asset =>
            {
                string path = asset.Replace(g_BaseTileObjPath, "mm_tiles/").Replace(".asset", ".png");
                Tile tile = (Tile)AssetDatabase.LoadAssetAtPath(asset, typeof(Tile));

                if (tile == null)
                {
                    Debug.LogErrorFormat("asset not found at {0}", path);
                }

                return new SpriteLookUp.TileEntry()
                {
                    Path = path,
                    Tile = tile,
                };
            })
            .ToArray();
    }
}
