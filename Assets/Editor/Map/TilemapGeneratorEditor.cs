using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace MM26.Map
{
    [CustomEditor(typeof(TilemapGenerator))]
    public class TilemapGeneratorEditor : Editor
    {
        public TilemapGenerator current
        {
            get
            {
                return (TilemapGenerator)target;
            }
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Fill Tilemap"))
                current.FillTilemap();
            if (GUILayout.Button("Clear Tilemap"))
                current.ClearTilemap();

            if (GUILayout.Button("Set Tilemap Size"))
                current.SetCellBoundsByEditor();
        }
    }
}