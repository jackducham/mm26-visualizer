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

                return new SpriteLookUp.TileEntry()
                {
                    Path = path,
                    Tile = tile,
                };
            })
            .ToArray();
    }
}
