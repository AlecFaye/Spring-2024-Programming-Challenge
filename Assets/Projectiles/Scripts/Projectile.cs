using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private enum Direction
    {
        Horizontal,
        Vertical
    }

    [SerializeField] private Direction direction = Direction.Horizontal;
    [SerializeField] private float speed = 10;
    [SerializeField] private float acceleration = 100;
    [SerializeField] private int damage = 1;

    private Vector2 frameVelocity;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (direction == Direction.Horizontal)
            frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, speed, acceleration * Time.fixedDeltaTime);
        else if (direction == Direction.Vertical)
            frameVelocity.y = Mathf.MoveTowards(frameVelocity.y, speed, acceleration * Time.fixedDeltaTime);

        rb.velocity = frameVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable))
            damageable.TakeDamage(null, damage);
    }
}
