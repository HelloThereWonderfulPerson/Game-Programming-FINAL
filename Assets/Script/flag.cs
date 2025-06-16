using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class flag : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private AudioClip victorySound;

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
        if (collision.CompareTag("Player"))
        {
            playerMovement movement = collision.GetComponent<playerMovement>();
            if (movement != null)
            {
                movement.enabled = false;
            }

            if (victorySound != null)
            {
                AudioSource.PlayClipAtPoint(victorySound, transform.position);
            }

            Invoke("ShowGameOverScreen", 0f);
        }
    }

    private void ShowGameOverScreen()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0f; 
        }
        else
        {
            Debug.LogWarning("No game over screen assigned!");
            SceneManager.LoadScene("MainMenu"); 
        }
    }
}
