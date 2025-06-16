using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class startMenuController : MonoBehaviour
{
    [SerializeField] private TMP_Text menuHighScoreText; // Assign in inspector

    private void Start()
    {
        UpdateMenuHighScoreDisplay();
    }

    public void OnStartClick()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void OnResetClick()
    {
        PlayerPrefs.DeleteKey("HighScores");
        PlayerPrefs.Save();
        UpdateMenuHighScoreDisplay();
    }

    private void UpdateMenuHighScoreDisplay()
    {
        if (menuHighScoreText)
            menuHighScoreText.text = $"High: {PlayerPrefs.GetInt("HighScore", 0)}";
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}