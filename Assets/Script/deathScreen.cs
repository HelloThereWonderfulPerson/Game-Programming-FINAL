using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class deathScreen : MonoBehaviour
{
    [SerializeField] private GameObject deathCanvas;

    // Start is called before the first frame update
    void Start()
    {
        deathCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDeathScreen()
    {
        Debug.Log("MASUk!");
        Time.timeScale = 0f; // Pause the game
        deathCanvas.SetActive(true);
    }

    public void OnRestartClick()
    {
        Time.timeScale = 1f; // Unpause before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMainMenuClick()
    {
        Time.timeScale = 1f; // Unpause before main menu SO IT DOESNT FREEZES
        SceneManager.LoadScene("StartScene");
    }
}
