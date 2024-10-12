using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Variables
    public float coins = 0;
    public float targetCoins = 0;
    public bool gamePaused = false;
    
    // Game Object References
    public TMP_Text coinsText;
    public GameObject pauseScreen;
    public GameObject winScreen;
    private AudioSource audioSource;
    public GameObject player;
    public GameObject winParticles;
    
    // Audio
    public AudioClip coinSound;
    public AudioClip buttonSound;
    public AudioClip winSound;

    void Start()
    {
        // Setting Initial Coin Text
        coinsText.text = Mathf.Round(coins).ToString();
        targetCoins = 69;
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        // Updating Coin Value and Text
        if (coins != targetCoins)
        {
            coins = Mathf.MoveTowards(coins, targetCoins, 0.25f);
            coinsText.text = Mathf.Round(coins).ToString();
        }
        
        // Setting Pause Variable and Time Speed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePaused = !gamePaused;
            pauseScreen.SetActive(gamePaused);
            Time.timeScale = Convert.ToInt32(!gamePaused);
        }
    }

    public void MenuReturn()
    {
        audioSource.PlayOneShot(buttonSound);
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void collectCoin()
    {
        if (targetCoins < 70)
            // Normal Coin System
            audioSource.PlayOneShot(coinSound);
        else
        {
            // Win Coin System
            audioSource.PlayOneShot(winSound);
            player.GetComponent<Rigidbody2D>().simulated = false;
            winScreen.SetActive(true);
            Instantiate(winParticles, player.transform.position + new Vector3(0, 3f, 0), Quaternion.identity);
        }
    }
}
