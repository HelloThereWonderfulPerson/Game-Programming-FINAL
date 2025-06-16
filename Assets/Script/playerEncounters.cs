using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerEncounters : MonoBehaviour
{
    public int playerMaxHealth = 3;
    public int playerCurrentHealth;
    [SerializeField] private float invincibilityTime = 1f;
    [SerializeField] private float invincibleUntil;


    [SerializeField] private LayerMask enemyLayer;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 20f;
    public float fireRate = 0.5f;

    [SerializeField] private float nextFireTime = 0f;
    [SerializeField] private bool facingRight = true;

    [SerializeField] private deathScreen deathScreen;

    audioManager AudioManager;

    private void Awake()
    {
        AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    public void TakeDamage(int damage)
    {

        AudioManager.PlaySFX(AudioManager.takeDamage);
        if (Time.time < invincibleUntil) return;

        playerCurrentHealth -= damage;
        invincibleUntil = Time.time + invincibilityTime;

        Debug.Log("Player health: " + playerCurrentHealth);

        // Visual feedback
        StartCoroutine(FlashPlayer());

        if (playerCurrentHealth <= 0)
        {
            Die();
        }
    }

    System.Collections.IEnumerator FlashPlayer()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        for (int i = 0; i < 3; i++)
        {
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check for direction change (assuming you have movement controls)
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            facingRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            facingRight = true;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        if(transform.position.y < -10)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if collided object is on the Enemy layer
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            TakeDamage(1);
        }
    }

    private void Die()
    {
        AudioManager.PlaySFX(AudioManager.death);
        Debug.Log("Player Died!");
        // Add death effects (e.g., particle explosion, sound)
        Destroy(gameObject); // Remove player (or respawn instead)
        deathScreen.ShowDeathScreen();
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Shoot in the direction player is facing
        Vector2 shootDirection = facingRight ? Vector2.right : Vector2.left;
        rb.AddForce(shootDirection * bulletForce, ForceMode2D.Impulse);

        // Optional: Destroy bullet after some time if it doesn't hit anything
        Destroy(bullet, 2f);
    }

}
