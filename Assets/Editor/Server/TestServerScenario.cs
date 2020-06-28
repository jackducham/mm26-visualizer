using MM26.IO.Models;

[TestScenario(Name = "Test Server")]
public class TestServerScenario : TestScenario
{
    public override void Start(TestServer server)
    {
        var state = new GameState()
        {
            StateId = 17
        };

        server.SetState(state);

        for (int i = 0; i < 18; i++)
        {
            server.Add(new GameChange()
            {
                ChangeId = i
            });
        }
    }
}
