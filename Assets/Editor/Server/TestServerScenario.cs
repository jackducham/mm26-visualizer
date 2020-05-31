using UnityEngine;

[TestScenario(Name = "Test Server")]
public class TestServerScenario : TestScenario
{
    public override void Start(TestServer server)
    {
        Debug.Log("Scenario Started!");
    }
}
