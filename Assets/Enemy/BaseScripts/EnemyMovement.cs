using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public delegate void EnemyMovementEvent();
    public EnemyMovementEvent OnReachedDestination;

    private const float EPSILON = 0.1f;

    [SerializeField] private Enemy enemy;
    [SerializeField] private float speed = 10.0f;

    private Vector2 destination;

    private bool hasReachedDestination = true;
    private float defaultSpeed;

    private void Awake()
    {
        defaultSpeed = speed;
        destination = transform.position;
    }

    private void OnEnable()
    {
        speed = defaultSpeed;
        hasReachedDestination = true;
    }

    private void Update()
    {
        if (enemy.EnemyStats.IsDead)
            return;

        if (!hasReachedDestination)
            Move();
    }

    public void SetDestination(Vector2 destination)
    {
        this.destination = destination;
        hasReachedDestination = false;

        enemy.EnemyAnimationController.SetAnimatorBool(EnemyAnimatorParameter.IsMoving, true);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void ResetSpeed()
    {
        speed = defaultSpeed;
    }

    private void Move()
    {
        if (Vector2.Distance(destination, (Vector2)transform.position) > EPSILON)
        {
            float step = speed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, destination, step);
        }
        else
        {
            enemy.EnemyAnimationController.SetAnimatorBool(EnemyAnimatorParameter.IsMoving, false);

            hasReachedDestination = true;

            OnReachedDestination?.Invoke();
        }
    }
}
