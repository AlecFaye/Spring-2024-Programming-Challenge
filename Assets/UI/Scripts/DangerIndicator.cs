using UnityEngine;

public class DangerIndicator : MonoBehaviour
{
    private readonly float displayTime = 1.5f;
    private float time;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        time = 0.0f;
    }

    private void Update()
    {
        if (time < displayTime)
            time += Time.deltaTime;
        else
            gameObject.SetActive(false);
    }
}
