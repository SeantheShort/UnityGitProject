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
    
    // Game Object References
    public TMP_Text coinsText;
    public RectTransform menuButton;
    public GameObject winText;
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
    }

    public void MenuReturn()
    {
        audioSource.PlayOneShot(buttonSound);
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
            winText.SetActive(true);
            menuButton.anchoredPosition = new Vector2(0, -300);
            Instantiate(winParticles, player.transform.position + new Vector3(0, 3f, 0), Quaternion.identity);
        }
    }
}
