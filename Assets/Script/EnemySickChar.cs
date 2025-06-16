using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySickChar : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    public GameObject coughPrefab;
    [SerializeField] private Transform firePoint;    
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float bulletSpeed = 5f;

    private float nextFireTime;
    private enemyMovement direction;

    scoreManager ScoreManager;

    private void Awake()
    {
        ScoreManager = FindObjectOfType<scoreManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        direction = GetComponent<enemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        //Debug.Log(direction.currentDirection);
        Vector2 shootDirection = new Vector2(direction.currentDirection, 0);

        GameObject bullet = Instantiate(coughPrefab, firePoint.position, firePoint.rotation);

        // Move bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = shootDirection * bulletSpeed;

        Destroy(bullet, 3f);

        Debug.Log("Hello");
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Hit!");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        ScoreManager.AddScore(20);
    }
}
