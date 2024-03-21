using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
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
        frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, speed, acceleration * Time.fixedDeltaTime);
     
        rb.velocity = frameVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable))
            damageable.TakeDamage(null, damage);
    }
}
