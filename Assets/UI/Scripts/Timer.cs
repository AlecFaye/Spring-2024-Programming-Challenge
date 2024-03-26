using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }

    private TextMeshProUGUI timerText;

    private float timeValue = 0f;

    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        DisplayTime();

        Instance = this;
    }

    private void Update()
    {
        UpdateTime();
        DisplayTime();
    }

    private void UpdateTime()
    {
        timeValue += Time.deltaTime;
    }

    private void DisplayTime()
    {
        float seconds = Mathf.FloorToInt(timeValue % 60);
        float minutes = Mathf.FloorToInt(timeValue / 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public string GetTimerText()
    {
        float seconds = Mathf.FloorToInt(timeValue % 60);
        float minutes = Mathf.FloorToInt(timeValue / 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
