using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MM26.Components;

public class mainLoader : MonoBehaviour
{
    public MM26.SceneConfiguration _sceneConfiguration;
    public GameObject url;
    public GameObject boardName;
    public void LoadScene() {
        _sceneConfiguration.WebSocketURL = url.GetComponent<Text>().text;
        _sceneConfiguration.BoardName = url.GetComponent<Text>().text;
       SceneManager.LoadScene("Main");
    }
    public void updateText() {
        _sceneConfiguration.WebSocketURL = url.GetComponent<Text>().text;
        _sceneConfiguration.BoardName = url.GetComponent<Text>().text;
    }
}
