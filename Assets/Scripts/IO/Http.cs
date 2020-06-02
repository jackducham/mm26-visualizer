using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public static class Http
{
    public static void Post<TBody>(string url, in TBody body)
    {
        Http.Post(url, body, (downloaded) => { });
    }

    public static void Post<TBody>(string url, in TBody body, Action<byte[]> onComplete)
    {
        string json = JsonUtility.ToJson(body);
        byte[] jsonData = Encoding.UTF8.GetBytes(json);

        UnityWebRequest request = UnityWebRequest.Post(url, "");

        request.uploadHandler = new UploadHandlerRaw(jsonData);
        request.SetRequestHeader("Content-Type", "application/json");

        AsyncOperation sendOperation = request.SendWebRequest();

        sendOperation.completed += (operation) =>
        {
            if (request.isNetworkError)
            {
                Debug.LogErrorFormat("Network Error: {0}", request.error);
                return;
            }

            if (request.isHttpError)
            {
                Debug.LogErrorFormat("Http Error: {0}", request.error);
                return;
            }

            onComplete(request.downloadHandler.data);
        };
    }
}
