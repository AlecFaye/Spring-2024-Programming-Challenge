using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private const float EPSILON = 0.1f;

    [SerializeField] private float speed = 10.0f;

    private Vector2 destination;

    private void Awake()
    {
        destination = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        Move();
    }

    private void Move()
    {
        if (Vector2.Distance(destination, (Vector2)transform.position) > EPSILON)
        {
            float step = speed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, destination, step);
        }
    }
}
