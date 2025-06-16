using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankChar : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] SpriteRenderer spriteRenderer;

    scoreManager ScoreManager;

    private void Awake()
    {
        ScoreManager = FindObjectOfType<scoreManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Wowza!");
        currentHealth -= damage;

        if(currentHealth == 1)
        {
            spriteRenderer.color = Color.white;
        }
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        ScoreManager.AddScore(15);
    }
}
