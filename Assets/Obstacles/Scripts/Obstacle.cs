using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Obstacle : MonoBehaviour
{
    private const float BUFFER = 5.0f;

    [SerializeField] private float speed = 10;
    [SerializeField] private int damage = 1;

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
        if (collision.TryGetComponent(out IDamageable damageable))
            damageable.TakeDamage(null, damage);
    }

    private void CheckDeactivate()
    {
        if (transform.position.x < -((Camera.main.orthographicSize * 2) + BUFFER))
            gameObject.SetActive(false);
    }
}
