using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float damageCooldown = 1f; 

    private float lastDamageTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time < lastDamageTime + damageCooldown) return;
        
        if (collision.CompareTag("Player"))
        {
            playerEncounters health = collision.GetComponent<playerEncounters>();
            
            if (health != null)
            {
                health.TakeDamage(damage);
                lastDamageTime = Time.time;
                
                //knockback
                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                    rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}
