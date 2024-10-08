using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Variables
    public float coins = 0;
    public float targetCoins = 0;
    
    // Game Object References
    public TMP_Text coinsText;

    void Start()
    {
        // Setting Initial Coin Text
        coinsText.text = Mathf.Round(coins).ToString();
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
}
