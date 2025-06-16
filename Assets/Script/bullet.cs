using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    //[SerializeField] private float speed = 10f;
    [SerializeField] private float spinSpeed = 500f;

    audioManager AudioManager;
    
    private void Awake()
    {
        AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector2.up * speed * Time.deltaTime);
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            AudioManager.PlaySFX(AudioManager.cureWhale);
            EnemyChar enemy = collision.GetComponent<EnemyChar>();
            EnemyTankChar tank = collision.GetComponent<EnemyTankChar>();
            EnemySickChar sick = collision.GetComponent<EnemySickChar>();
            //scoreManager score = collision.GetComponent<scoreManager>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("enemy");
            } 
            else if (tank != null)
            {
                tank.TakeDamage(damage);
                Debug.Log("tank");
            }
            else if (sick != null)
            {
                sick.TakeDamage(damage);
                Debug.Log("sick");
            } 


            Destroy(gameObject);
        }
        else if (!collision.CompareTag("Player") && !collision.CompareTag("Bullet"))
        {
            // Destroy bullet when hitting walls/obstacles
            Destroy(gameObject);
        }
    }

}
