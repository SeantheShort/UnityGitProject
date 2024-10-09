using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    // Variables
    public int coinValue;
    private float initialY;
    
    // Game Object References
    public Sprite oneCoin, fiveCoin, tenCoin;
    public SpriteRenderer spriteRenderer;
    
    public UIManager coinManager;
    
    void Start()
    {
        // Getting Initial Y Position
        initialY = transform.position.y;
        
        // Determing Coin Sprite
        switch (coinValue)
        {
            case 1: spriteRenderer.sprite = oneCoin; break;
            case 5: spriteRenderer.sprite = fiveCoin; break;
            case 10: spriteRenderer.sprite = tenCoin; break;
        }
    }
    
    void Update()
    {
        transform.position = new Vector3(transform.position.x, initialY + 0.25f*Mathf.Sin(Time.time * 3f), transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            coinManager.collectCoin();
            coinManager.targetCoins += coinValue;
            Destroy(gameObject);
        }
    }
}
