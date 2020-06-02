[TestScenario(Name = "Test Server")]
public class TestServerScenario : TestScenario
{
    public override void Start(TestServer server)
    {
        server.State = new byte[] { 17 };
        server.AddChange(new byte[] { 177 }, 0.1f);
        server.AddChange(new byte[] { 178 }, 1.0f);
    }
}
