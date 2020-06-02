using System;
using System.Collections.Generic;

/// <summary>
/// Test server
/// </summary>
public class TestServer
{
    [Serializable]
    private struct Change
    {
        public float delay;
        public byte[] data;
    }

    [Serializable]
    private struct Configuration
    {
        public byte[] state;
        public Change[] changes;
    }

    public byte[] State { get; set; }
    private List<Change> _changes = new List<Change>();

    /// <summary>
    /// Construct a test server instance
    /// </summary>
    public TestServer()
    {
    }

    /// <summary>
    /// Send the configuration to the server
    /// </summary>
    /// <param name="port"></param>
    public void SendConfiguration(int port)
    {
        var configuration = new Configuration()
        {
            state = this.State,
            changes = new Change[]
            {
                new Change()
                {
                    data = new byte[]{ 17 }
                }

            }
        };

        Http.Post($"http://localhost:{port}/configure/", configuration);
    }

    public void AddChange(byte[] data, float delay = 0.0f)
    {
        _changes.Add(new Change() { data = data, delay = delay });
    }
}
