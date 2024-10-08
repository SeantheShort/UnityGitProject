using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    // Variables
    private float horizontalMovement;
    public float playerSpeed;
    public float jumpForce;
    private float jumpTimer = 1;
    public bool onGround;
    private float fallTimer = 0f;
    public Vector3 respawnPos;
    
    // GameObject References
    public Rigidbody2D rb;
    public GameObject playerSprite;
    public GameObject deathParticles;

    void Start()
    {
        respawnPos = transform.position;
    }

    void Update()
    {
        // Checking if onGround
        onGround = Physics2D.CircleCast(transform.position, 0.1f, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        
        // Controlling Horizontal Movement
        horizontalMovement = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalMovement * playerSpeed, rb.velocity.y);
        
        //Jump Timing
        if (Input.GetButton("Jump") && jumpTimer <= 2f && onGround)
        {
            jumpTimer += Time.deltaTime;
            playerSprite.transform.localScale += new Vector3(1f, -0.75f, 0) * Time.deltaTime;
            
        }
        else if (onGround) // Returning Sprite to Original Size
        {
            playerSprite.transform.localScale = Vector3.Lerp(playerSprite.transform.localScale, Vector3.one, Time.deltaTime * 4);
        }
        
        // Jumping
        if ((Input.GetButtonUp("Jump") || jumpTimer >= 2) && onGround)
        {
            playerSprite.transform.localScale = new Vector3(1, 1, 1);
            rb.AddForce(new Vector2(0f, jumpForce * jumpTimer));
            jumpTimer = 1;
        }
        
        // Land Squash Effect
        if (!onGround && rb.velocity.y < 0 && fallTimer <= 2){ fallTimer += Time.deltaTime; }

        if (onGround && fallTimer > 0)
        {
            playerSprite.transform.localScale = new Vector3(1 + fallTimer, Mathf.Max(1/(fallTimer/2 + 1), 0.1f), 1);
            fallTimer = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Death Collision
        if (other.gameObject.layer == 7)
        {
            Debug.Log("Player Death");
            StartCoroutine(playerDeath());

        }
    }

    IEnumerator playerDeath()
    {
        // Summons Particles & Disables Player
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        playerSprite.GetComponent<SpriteRenderer>().enabled = false;
        
        // Waits a second
        yield return new WaitForSeconds(1);
        
        // Returns player to respawn position and enables player
        transform.position = respawnPos;
        playerSprite.GetComponent<SpriteRenderer>().enabled = true;
        playerSprite.transform.localScale = new Vector3(1, 1, 1);
        rb.isKinematic = false;
        
    }
}
