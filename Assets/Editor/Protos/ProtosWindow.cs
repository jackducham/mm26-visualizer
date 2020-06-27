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

    public static void Generate(string inDir, string protocPath)
    {
        string outputFolder = Path.Combine(Application.dataPath, "Scripts", "IO", "Models");

        UnityEngine.Debug.LogFormat("Cleans {0}", outputFolder);

        if (Directory.Exists(outputFolder))
        {
            Directory.Delete(outputFolder, true);
        }

        Directory.CreateDirectory(outputFolder);

        IEnumerable<string> files = Directory.EnumerateFiles(inDir)
                .Where(file => file.EndsWith(".proto"));

        ProcessStartInfo startInfo = new ProcessStartInfo()
        {
            FileName = protocPath,
            Arguments = string.Format(
                "--proto_path={0} --csharp_out={1} {2}",
                inDir,
                outputFolder,
                string.Join(" ", files)),

            RedirectStandardError = true,
            RedirectStandardOutput = true,
            UseShellExecute = false,
        };

        UnityEngine.Debug.Log("Genearting Protocol Buffer");

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
            UnityEngine.Debug.LogError(standardError);
        }

        UnityEngine.Debug.Log("Done");
    }

    private string _protosFolder = ".";
    private string _protocPath = "";

    private void ApplyDefaults()
    {
        if (_protocPath == "")
        {
#if UNITY_EDITOR_OSX
            _protocPath = "/usr/local/bin/protoc";
#endif
        }
    }

    private void OnGUI()
    {
        this.ApplyDefaults();

        EditorGUILayout.LabelField("Protos Folder", _protosFolder);
        _protocPath = EditorGUILayout.TextField("\"protoc\" Path", _protocPath);

        if (GUILayout.Button("Select Protos Folder"))
        {
            _protosFolder = EditorUtility.OpenFolderPanel("Select Proto Files", ".", "");
        }

        if (GUILayout.Button("Generate"))
        {
            ProtosWindow.Generate(_protosFolder, _protocPath);
        }
    }
}
