using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class TilesImport
{

    [MenuItem("Assets/Import Tiles")]
    public static void ImportTiles()
    {
        var tiles = Directory.EnumerateFiles("Assets/Sprites/mm26_tiles/raw")
            .Where(path => Path.GetExtension(path) != ".meta")
            .Select(path =>
            {
                string name = Path.GetFileNameWithoutExtension(path);
                var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);

                return (name, sprite);
            })
            .Select(item =>
            {
                var tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = item.sprite;

                return (tile, item.name);
            });

        foreach (var tile in tiles)
        {
            string path = Path.Combine("Assets/Sprites/mm26_tiles", $"{tile.name}.asset");

            AssetDatabase.CreateAsset(tile.tile, path);

            Debug.Log(path);
        }

        AssetDatabase.SaveAssets();
    }
}
