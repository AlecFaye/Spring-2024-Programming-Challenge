using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float parallaxEffect;
    [SerializeField] private float speed = 10.0f;

    private float backgroundLength;
    private float xDelta;

    private void Start()
    {
        backgroundLength = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        UpdateParallax();
        OverlapParallax();

        xDelta += speed * Time.deltaTime;
    }

    private void UpdateParallax()
    {
        transform.position = new(xDelta * parallaxEffect, transform.position.y, transform.position.z);
    }

    private void OverlapParallax()
    {
        if (xDelta * parallaxEffect > backgroundLength)
        {
            xDelta = 0.0f;
        } 
        else if (xDelta * parallaxEffect < -backgroundLength)
        {
            xDelta = 0.0f;
        }
    }
}
