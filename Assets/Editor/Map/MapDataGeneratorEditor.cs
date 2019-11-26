using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace MM26.Map
{
    [CustomEditor(typeof(MapDataGenerator))]
    public class MapGeneratorEditor : Editor
    {
        public MapDataGenerator current
        {
            get
            {
                return (MapDataGenerator)target;
            }
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Read Map from File"))
                current.ReadMapFrom(0);
            if (GUILayout.Button("Read Map from Tilemap"))
                current.ReadMapFrom(1);
        }
    }
}