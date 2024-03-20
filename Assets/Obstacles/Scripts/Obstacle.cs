using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Obstacle : MonoBehaviour
{
    private const float BUFFER = 5.0f;

    [SerializeField] private float speed = 10;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new(speed, 0.0f);

        CheckDeactivate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }

    private void CheckDeactivate()
    {
        if (transform.position.x < -((Camera.main.orthographicSize * 2) + BUFFER))
            gameObject.SetActive(false);
    }
}
