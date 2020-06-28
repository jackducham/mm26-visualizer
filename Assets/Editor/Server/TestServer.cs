using System;
using System.Collections.Generic;
using Google.Protobuf;
using UnityEngine;

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

    private byte[] _state = null;
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
            state = _state,
            changes = _changes.ToArray()
        };

        Http.Post($"http://localhost:{port}/configure/", configuration);
    }

    public void Add<T>(T change, float delay = 0.0f) where T: IMessage<T>
    {
        _changes.Add(new Change()
        {
            data = change.ToByteArray(),
            delay = delay
        });
    }

    public void SetState<T>(T state) where T: IMessage<T>
    {
        _state = state.ToByteArray();
    }
}
