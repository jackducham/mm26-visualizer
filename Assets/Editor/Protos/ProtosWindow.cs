using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class ProtosWindow : EditorWindow
{
    [MenuItem("Window/Protos")]
    public static void OpenProtosConfiguration()
    {
        GetWindow<ProtosWindow>("Protos");
    }

    private string _protosFolder = ".";
    private string _protocPath = "protoc";

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Protos Folder", _protosFolder);
        _protocPath = EditorGUILayout.TextField("\"protoc\" Path", "protoc");

        if (GUILayout.Button("Select Protos Folder"))
        {
            _protosFolder = EditorUtility.OpenFolderPanel("Select Proto Files", ".", "");
        }

        if (GUILayout.Button("Generate"))
        {
            IEnumerable<string> files = Directory.EnumerateFiles(_protosFolder)
                .Where(file => file.EndsWith(".proto"));

            string outputFolder = Path.Combine(Application.dataPath, "Scripts", "IO", "Models");

            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = _protocPath,
                Arguments = string.Format(
                    "--proto_path={0} --csharp_out={1} {2}",
                    _protosFolder,
                    outputFolder,
                    string.Join(" ", files)),

                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
            };

            Process process = Process.Start(startInfo);
            process.WaitForExit();

            string standardOutput = process.StandardOutput.ReadToEnd();
            string standardError = process.StandardError.ReadToEnd();

            if (standardOutput != "")
            {
                UnityEngine.Debug.Log(standardOutput);
            }

            if (standardError != "")
            {
                UnityEngine.Debug.Log(standardError);
            }
        }
    }
}
