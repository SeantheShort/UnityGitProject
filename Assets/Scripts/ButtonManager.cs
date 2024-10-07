using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // Quit Button
    public void QuitGame()
    {
        Debug.Log("Game Quit!");
        Application.Quit();
    }
    
    // Scene Switch Button
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
