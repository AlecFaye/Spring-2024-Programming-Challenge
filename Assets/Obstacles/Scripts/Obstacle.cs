using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody2D))]
public class Obstacle : MonoBehaviour
{
    private const float BUFFER = 5.0f;

    [SerializeField] private float speed = 10;

    private ObjectPool<Obstacle> pool;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new(speed * Time.fixedDeltaTime, rb.velocity.y);

        CheckDeactivate();
    }

    private void OnDisable()
    {
        pool.Release(this);
    }

    public void SetPool(ObjectPool<Obstacle> pool)
    {
        this.pool = pool;
    }

    private void CheckDeactivate()
    {
        if (transform.position.x < -((Camera.main.orthographicSize * 3) + BUFFER))
            gameObject.SetActive(false);
    }
}
