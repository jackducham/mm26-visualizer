using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace MM26.Board.Helper
{
    [CustomEditor(typeof(TileDatabase))]
    public class TileDatabaseEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            TileDatabase myScript = (TileDatabase)target;

            GUILayout.Label("Remember to run this the first time it's added to a scene!");
            if (GUILayout.Button("Populate TileDatabase"))
            {
                myScript.PopulateDictionary();
            }

            if (GUILayout.Button("Test TileDatabase"))
            {
                myScript.TestDatabase();
            }
        }
    }
}