using System;
using System.Threading.Tasks;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class TestScenarioAttribute : Attribute
{
    public string Name { get; set; }
}

/// <summary>
/// A test scenario
/// </summary>
public abstract class TestScenario
{
    /// <summary>
    /// Invoked when the scenario is started
    /// </summary>
    /// <param name="server"></param>
    public abstract void Start(TestServer server); 
}
