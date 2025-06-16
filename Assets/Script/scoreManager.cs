using TMPro;
using UnityEngine;

public class scoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text currentScoreText;
    [SerializeField] private TMP_Text highScoreText;

    private int currentScore;
    private const string HIGH_SCORE_KEY = "HighScores";

    private void Start()
    {
        UpdateScoreDisplays();
    }

    public void AddScore(int points)
    {
        currentScore += points;

        // Check and update high score
        int savedHigh = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        if (currentScore > savedHigh)
        {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, currentScore);
        }

        UpdateScoreDisplays();
    }

    private void UpdateScoreDisplays()
    {
        if (currentScoreText) currentScoreText.text = $"Score: {currentScore}";
        if (highScoreText) highScoreText.text = $"Highscore: {PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0)}";
    }

    public void ResetCurrentScore()
    {
        currentScore = 0;
        UpdateScoreDisplays();
    }
}