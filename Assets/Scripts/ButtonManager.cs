using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // Variables
    private float alphaGoal = 0;
    private float currentAlpha = 0;
    
    // Game Object References
    public Image transitionScreen;
    public AudioSource audioSource;

    void Update()
    {
        // Updating Transition Screen Alpha
        if (alphaGoal != 0)
        {
            // Lerping
            currentAlpha = Mathf.MoveTowards(currentAlpha, alphaGoal, Time.deltaTime * 5);
            transitionScreen.color = new Color(0.6431373f, 0.7843138f, 1, currentAlpha);
            
            // Changing Scene
            if (Mathf.Abs(alphaGoal-currentAlpha) < 0.01f)
                SceneManager.LoadScene("Gameplay");
        }
        
    }

    // Quit Button
    public void QuitGame()
    {
        audioSource.Play();
        Debug.Log("Game Quit!");
        Application.Quit();
    }
    
    // Scene Switch Button
    public void SwitchScene()
    {
        // Activating Transition
        audioSource.Play();
        alphaGoal = 1;
    }
}
