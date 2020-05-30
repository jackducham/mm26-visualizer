using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class TestServer : EditorWindow
{
    [MenuItem("Window/Test Server")]
    public static void OpenTestServer()
    {
        TestServer server = GetWindow<TestServer>("Test Server");

        server.GenerateScenarios();
    }

    enum State
    {
        Idle,
        Running,
    }

    private State _state = State.Idle;

    private Dictionary<string, Type> _scenarios;
    private string[] _scenarioNames;

    private void OnGUI()
    {
        switch (_state)
        {
            case State.Idle:
                EditorGUILayout.LabelField("Status", "Idle");

                EditorGUILayout.Popup("Scenario", 0, _scenarioNames);

                if (GUILayout.Button("Start Server"))
                {
                    _state = this.StartServer();
                }

                break;
            case State.Running:
                EditorGUILayout.LabelField("Status", "Running");

                if (GUILayout.Button("Shutdown Server"))
                {
                    _state = this.ShutdownServer();
                }
                break;
        }

        if (GUILayout.Button("Re-scan Codebase for Scenarios"))
        {
            this.GenerateScenarios();
        }
    }

    private void GenerateScenarios()
    {
        _scenarios = new Dictionary<string, Type>();
        List<string> scenarioNames = new List<string>();

        Assembly assembly = Assembly.GetCallingAssembly();
        IEnumerable<Type> scenarioTypes = assembly.GetTypes()
            .Where((Type type) => { return type.IsSubclassOf(typeof(TestScenario)); });

        foreach (Type type in scenarioTypes)
        {
            foreach (TestScenarioAttribute attribute in type.GetCustomAttributes<TestScenarioAttribute>())
            {
                _scenarios[attribute.Name] = type;
                scenarioNames.Add(attribute.Name);
            }
        }

        _scenarioNames = scenarioNames.ToArray();
    }

    private State StartServer()
    {
        return State.Running;
    }

    private State ShutdownServer()
    {
        return State.Idle;
    }
}
