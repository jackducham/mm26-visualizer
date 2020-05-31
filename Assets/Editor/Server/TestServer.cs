using System.Net;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Test server
/// </summary>
public class TestServer
{
    private HttpListener _listener = null;

    /// <summary>
    /// Construct a test server instance
    /// </summary>
    public TestServer()
    {
    }

    /// <summary>
    /// Start the server on a port
    /// </summary>
    /// <param name="port"></param>
    public void Start(int port)
    {
        _listener = new HttpListener();
        _listener.Prefixes.Add($"http://localhost:{port}/");
        _listener.Start();

        _listener.GetContextAsync().ContinueWith(task =>
        {
            HttpListenerContext context = task.Result;

            if (context.Request.IsWebSocketRequest)
            {
                Debug.Log("Is web socket");
            }
            else
            {
                Debug.LogError("Request is not websocket");
            }
        });
    }

    /// <summary>
    /// Stop the server
    /// </summary>
    public void Stop()
    {
        _listener.Stop();
    }
}
