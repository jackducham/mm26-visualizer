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

            if (GUILayout.Button("Populate TileDatabase"))
            {
                myScript.populateDictionary();
            }
        }
    }
}