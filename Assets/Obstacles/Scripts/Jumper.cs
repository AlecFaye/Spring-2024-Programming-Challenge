using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float jumpForce = 150.0f;

    private Rigidbody2D rb;

    private float time = 0.0f;

    private void OnEnable()
    {
        time = 0.0f;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (time >= jumpCooldown)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            time = 0.0f;
        }
    }
}
