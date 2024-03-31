using System.Collections;
using TMPro;
using UnityEngine;

public class DeathPanelUI : MonoBehaviour
{
    private const float DELAY = 0.5f;

    [Header("Time")]
    [SerializeField] private TextMeshProUGUI timeAmountText;
    [SerializeField] private TextMeshProUGUI timeScoreText;
    [SerializeField] private int scorePerSecond = 100;

    [Header("Coins")]
    [SerializeField] private TextMeshProUGUI coinsAmountText;
    [SerializeField] private TextMeshProUGUI coinsMultiplierText;
    [SerializeField] private float multiplierPerCoin = 0.1f;

    [Header("Total")]
    [SerializeField] private TextMeshProUGUI totalScoreText;

    [Header("Other")]
    [SerializeField] private GameObject buttonsObject;

    private int timeScore;
    private float coinsMultiplier = 1.0f;

    private void OnEnable()
    {
        buttonsObject.SetActive(false);

        timeAmountText.text = GetTimerText((int)GameTimer.Instance.CurrentTimeInSeconds);
        coinsAmountText.text = $"{Player.Instance.PlayerStats.GetBalance()}";

        timeScoreText.text = "";
        coinsMultiplierText.text = "";
        totalScoreText.text = "";

        Application.runInBackground = true;
        Application.targetFrameRate = 60;

        StartCoroutine(CalculateTimeScore());
    }

    private IEnumerator CalculateTimeScore()
    {
        yield return new WaitForSecondsRealtime(DELAY);

        float timeInSeconds = GameTimer.Instance.CurrentTimeInSeconds;
        timeScore = (int)timeInSeconds * scorePerSecond;
        timeScoreText.text = $"{timeScore}";

        StartCoroutine(CalculateCoinsMultiplier());
    }
    
    private IEnumerator CalculateCoinsMultiplier()
    {
        yield return new WaitForSecondsRealtime(DELAY);

        int coins = Player.Instance.PlayerStats.GetBalance();
        coinsMultiplier = 1.0f + coins * multiplierPerCoin;
        coinsMultiplierText.text = $"x{coinsMultiplier}";

        StartCoroutine(CalculateTotalScore());
    }

    private IEnumerator CalculateTotalScore()
    {
        yield return new WaitForSecondsRealtime(DELAY);

        int totalScore = (int)(timeScore * coinsMultiplier);
        totalScoreText.text = $"{totalScore}";

        yield return new WaitForSecondsRealtime(DELAY);
        
        buttonsObject.SetActive(true);
    }

    private string GetTimerText(float timeInSeconds)
    {
        float seconds = Mathf.FloorToInt(timeInSeconds % 60);
        float minutes = Mathf.FloorToInt(timeInSeconds / 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
