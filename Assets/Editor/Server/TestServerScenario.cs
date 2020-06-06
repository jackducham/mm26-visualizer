using MM26.IO.Models;

[TestScenario(Name = "Test Server")]
public class TestServerScenario : TestScenario
{
    public override void Start(TestServer server)
    {
        var state = new GameState()
        {
            StateId = 0
        };

        server.SetState(state);

        var change = new GameChange()
        {
            ChangeId = 0
        };

        server.Add(change);
    }
}
