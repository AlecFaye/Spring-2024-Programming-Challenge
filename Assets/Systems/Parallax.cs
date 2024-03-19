using UnityEngine;
using UnityEngine.Tilemaps;

public class Parallax : MonoBehaviour
{
    [SerializeField] private ParallaxImageType parallaxImageType = ParallaxImageType.SpriteRenderer;
    [SerializeField, Range(0, 1)] private float parallaxEffect;
    [SerializeField] private float speed = 10.0f;

    private float backgroundLength;
    private float xDelta;

    private enum ParallaxImageType
    {
        SpriteRenderer,
        Tilemap
    }

    private void Start()
    {
        switch (parallaxImageType)
        {
            case ParallaxImageType.SpriteRenderer:
                if (TryGetComponent(out SpriteRenderer spriteRenderer))
                    backgroundLength = spriteRenderer.bounds.size.x;
                break;
            case ParallaxImageType.Tilemap:
                if (TryGetComponent(out Tilemap tilemap))
                    backgroundLength = tilemap.cellBounds.size.x - 2;
                break;
        }
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
