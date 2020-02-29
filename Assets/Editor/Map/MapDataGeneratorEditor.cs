using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace MM26.Map
{
    [CustomEditor(typeof(MapDataGenerator))]
    public class MapGeneratorEditor : Editor
    {
        public MapDataGenerator Current
        {
            get
            {
                return (MapDataGenerator)target;
            }
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Save Map from File"))
                Current.ReadMapFrom(MapSource.File);
            if (GUILayout.Button("Save Map from Tilemap"))
                Current.ReadMapFrom(MapSource.Tilemap);
        }
    }
}
