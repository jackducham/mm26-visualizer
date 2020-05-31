using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Window for a test server
/// </summary>
public class TestServerWindow : EditorWindow
{
    [MenuItem("Window/Test Server")]
    public static void OpenTestServer()
    {
        TestServerWindow server = GetWindow<TestServerWindow>("Test Server");

        server.FindScenarios();
    }

    /// <summary>
    /// If scenario is running or not
    /// </summary>
    enum State
    {
        Idle,
        Running,
    }

    /// <summary>
    /// The server instance
    /// </summary>
    private static TestServer _server = null;

    /// <summary>
    /// The scenario instance
    /// </summary>
    private static TestScenario _scenario = null;

    /// <summary>
    /// Current state
    /// </summary>
    private static State _state = State.Idle;

    /// <summary>
    /// Port on which the scenario is running, (at localhost)
    /// </summary>
    private static string _port = "3000";

    /// <summary>
    /// String to Type mapping of all possible scenarios
    /// </summary>
    private Dictionary<string, Type> _scenarios;

    /// <summary>
    /// A list of scenario names
    /// </summary>
    private string[] _scenarioNames;

    /// <summary>
    /// Index of selection of scenario names
    /// </summary>
    private int _selectedScenario = 0;

    private void OnEnable()
    {
        //VisualElement root = this.rootVisualElement;
    }

    private void OnGUI()
    {
        switch (_state)
        {
            case State.Idle:
                EditorGUILayout.LabelField("Status", "Idle");

                _selectedScenario = EditorGUILayout.Popup("Scenario", _selectedScenario, _scenarioNames);
                _port = EditorGUILayout.TextField("Port", _port);

                if (GUILayout.Button("Start Server"))
                {
                    _state = this.StartServer(_scenarioNames[_selectedScenario]);
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
            this.FindScenarios();
        }
    }

    /// <summary>
    /// Find scenarios in current assembly
    /// </summary>
    private void FindScenarios()
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

    /// <summary>
    /// Start the server
    /// </summary>
    /// <param name="scenarioName">the name of the scenario</param>
    /// <returns>
    /// if succeeds, return <c>State.Running</c>, otherwise, returns
    /// <c>State.Idle</c>
    /// </returns>
    private State StartServer(string scenarioName)
    {
        try
        {
            _server = new TestServer();
            _server.Start(Convert.ToInt32(_port));

            _scenario = (TestScenario)Activator.CreateInstance(_scenarios[scenarioName]);
            _scenario.Start(_server);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return State.Idle;
        }

        return State.Running;
    }

    /// <summary>
    /// Shut down the server
    /// </summary>
    /// <returns></returns>
    private State ShutdownServer()
    {
        _server.Stop();
        return State.Idle;
    }
}
