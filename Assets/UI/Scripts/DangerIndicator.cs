using UnityEngine;

public class DangerIndicator : MonoBehaviour
{
    [SerializeField, Range(0, 255)] private float minOpacity = 10.0f;
    [SerializeField, Range(0, 255)] private float maxOpacity = 100.0f;
    [SerializeField, Range(1, 9)] private int numberOfFlickers = 3;
    [SerializeField] private float displayTime = 1.5f;

    private SpriteRenderer spriteRenderer;

    private float time;
    private float flickerTime = 0.0f;
    
    private int numberOfFlickersCompleted = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        flickerTime = displayTime / numberOfFlickers;

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        numberOfFlickersCompleted = 0;
        time = 0.0f;
    }

    private void Update()
    {
        if (numberOfFlickersCompleted >= numberOfFlickers)
            gameObject.SetActive(false);

        time += Time.deltaTime;

        spriteRenderer.color = new(
            spriteRenderer.color.r, 
            spriteRenderer.color.g, 
            spriteRenderer.color.b,
            minOpacity / 255.0f + time / flickerTime * (maxOpacity - minOpacity) / 255.0f);

        if (time > flickerTime)
        {
            time = 0.0f;
            numberOfFlickersCompleted++;
        }
    }
}
