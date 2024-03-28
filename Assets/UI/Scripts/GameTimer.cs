using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance { get; private set; }

    private TextMeshProUGUI timerText;

    public float CurrentTimeInSeconds { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("There is more than one Game Timer in the scene.");

        Instance = this;

        timerText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        CurrentTimeInSeconds = 0.0f;

        DisplayTime();
    }

    private void Update()
    {
        UpdateTime();
        DisplayTime();
    }

    private void UpdateTime()
    {
        CurrentTimeInSeconds += Time.deltaTime;
    }

    private void DisplayTime()
    {
        float seconds = Mathf.FloorToInt(CurrentTimeInSeconds % 60);
        float minutes = Mathf.FloorToInt(CurrentTimeInSeconds / 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public string GetTimerText()
    {
        float seconds = Mathf.FloorToInt(CurrentTimeInSeconds % 60);
        float minutes = Mathf.FloorToInt(CurrentTimeInSeconds / 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
