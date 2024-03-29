using UnityEngine;

public enum LaserType
{
    Horizontal,
    VerticalLeft,
    VerticalRight,
}

public class Laser : MonoBehaviour
{
    [SerializeField] private float disableTime = 0.5f;

    float time = 0.0f;

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
        if (time >= disableTime)
            gameObject.SetActive(false);
        else
            time += Time.deltaTime;
    }
}
