using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cough : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("1");
        if (collision.CompareTag("Player")) // Hurt player
        {
            Debug.Log("2");
            playerEncounters playerHealth = collision.GetComponent<playerEncounters>();

            playerHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (!collision.CompareTag("Enemy") && !collision.CompareTag("Cough")) // Destroy on walls
        {
            Debug.Log("3");
            Destroy(gameObject);
        }
    }

}
