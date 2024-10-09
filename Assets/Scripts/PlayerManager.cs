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
    private bool playerDead = false;
    public Vector3 respawnPos;
    
    // GameObject References
    public Rigidbody2D rb;
    public GameObject playerSprite;
    public GameObject deathParticles;
    private AudioSource audioSource;
    // Sounds
    public AudioClip jumpSound;
    public AudioClip jumpCharge;
    public AudioClip playerDie;
    public AudioClip playerLand;

    void Start()
    {
        respawnPos = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Checking if onGround
        onGround = Physics2D.CircleCast(transform.position, 0.05f, Vector2.down, 0.05f, LayerMask.GetMask("Ground"));
        
        // Controlling Horizontal Movement
        horizontalMovement = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalMovement * playerSpeed, rb.velocity.y);
        
        //Jump Timing
        if (Input.GetButton("Jump") && jumpTimer <= 2f && onGround)
        {
            // Play Charge Sound
            if (audioSource.clip != jumpCharge && !audioSource.isPlaying)
            {
                audioSource.clip = jumpCharge;
                audioSource.Play();
            }

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
            audioSource.Stop();
            audioSource.PlayOneShot(jumpSound);
            audioSource.clip = null;
            playerSprite.transform.localScale = new Vector3(1, 1, 1);
            rb.AddForce(new Vector2(0f, jumpForce * jumpTimer));
            jumpTimer = 1;
            
        }
        // Jumping when going off platform
        else if (!onGround && rb.velocity.y < 0 && jumpTimer > 1.1f)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(jumpSound);
            audioSource.clip = null;
            playerSprite.transform.localScale = new Vector3(1, 1, 1);
            rb.AddForce(new Vector2(0f, jumpForce * jumpTimer));
            jumpTimer = 1;
        }
        
        // Land Squash Effect
        if (!onGround && rb.velocity.y < 0 && fallTimer <= 2){ fallTimer += Time.deltaTime; }

        if (onGround && fallTimer > 0)
        {
            audioSource.PlayOneShot(playerLand, Mathf.Clamp(fallTimer, 0.1f, 1f));
            playerSprite.transform.localScale = new Vector3(1 + fallTimer, Mathf.Max(1/(fallTimer/2 + 1), 0.1f), 1);
            fallTimer = 0;
        }
        
        // Checking for boundary death
        if (transform.position.y <= -7.5f && !playerDead)
            StartCoroutine(playerDeath());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Death Collision
        if (other.gameObject.layer == 7)
            StartCoroutine(playerDeath());
        
        // Respawn Collision
        else if (other.CompareTag("Respawn"))
        {
            respawnPos = other.transform.position;
            Destroy(other.gameObject);
        }
    }

    IEnumerator playerDeath()
    {
        // Plays Sound
        audioSource.Stop();
        audioSource.PlayOneShot(playerDie);
        
        // Summons Particles & Disables Player
        playerDead = true;
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        rb.velocity = Vector2.zero;
        rb.simulated = false;
        playerSprite.GetComponent<SpriteRenderer>().enabled = false;
        
        // Waits a second
        yield return new WaitForSeconds(1);
        
        // Returns player to respawn position and enables player
        playerDead = false;
        transform.position = respawnPos;
        playerSprite.GetComponent<SpriteRenderer>().enabled = true;
        playerSprite.transform.localScale = new Vector3(1, 1, 1);
        rb.simulated = true;
        
    }
}
