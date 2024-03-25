using System.Collections;
using UnityEngine;

public class HoverSlamAttack : EnemyAbility
{
    [Header("References")]
    [SerializeField] private Enemy enemy;

    [Header("Hover Attack Configurations")]
    [SerializeField] private Destination hoverDestination;
    [SerializeField] private float hoverTime = 2.0f;
    [SerializeField] private float hoverSpeed = 20.0f;

    [Header("Slam Attack Configurations")]
    [SerializeField] private Destination slamDestination;
    [SerializeField] private Collider2D damageCollider;
    [SerializeField] private float slamDelay = 0.25f;
    [SerializeField] private float slamEndDelay = 0.25f;
    [SerializeField] private float slamSpeed = 50.0f;

    [SerializeField] private Destination endDestination;

    private void Start()
    {
        OnTriggerAbility += TriggerAbility;

        damageCollider.enabled = false;
    }

    public override void StartAbility()
    {
        base.StartAbility();

        enemy.EnemyAnimationController.SetAnimatorTrigger(EnemyAnimatorParameter.ChargeUpHover);
    }

    public override void TriggerAbility()
    {
        Vector2 destinationPosition = BossArena.Instance.GetDestination(hoverDestination);
        enemy.EnemyMovement.SetSpeed(hoverSpeed);
        enemy.EnemyMovement.SetDestination(destinationPosition);
        enemy.EnemyMovement.OnReachedDestination += Enemy_OnReachedHoverDestination;
    }

    private void Enemy_OnReachedHoverDestination()
    {
        enemy.EnemyMovement.OnReachedDestination -= Enemy_OnReachedHoverDestination;
        enemy.EnemyAnimationController.SetAnimatorTrigger(EnemyAnimatorParameter.Hover);

        StartCoroutine(FollowHover());
    }

    private IEnumerator FollowHover()
    {
        float time = 0.0f;

        Vector2 destinationPosition = BossArena.Instance.GetDestination(hoverDestination);

        while (time < hoverTime)
        {
            Vector2 hoverPosition = new(Player.Instance.transform.position.x, destinationPosition.y);
            enemy.EnemyMovement.SetDestination(hoverPosition);

            time += Time.deltaTime;

            yield return null;
        }

        StartCoroutine(Slam());
    }

    private IEnumerator Slam()
    {
        enemy.EnemyAnimationController.SetAnimatorTrigger(EnemyAnimatorParameter.Slam);

        yield return new WaitForSeconds(slamDelay);

        damageCollider.enabled = true;

        Vector2 destinationPosition = BossArena.Instance.GetDestination(slamDestination);
        Vector2 slamPosition = new(transform.position.x, destinationPosition.y);

        enemy.EnemyMovement.SetSpeed(slamSpeed);
        enemy.EnemyMovement.SetDestination(slamPosition);

        enemy.EnemyMovement.OnReachedDestination += Enemy_OnReachedSlamDestination;
    }

    private void Enemy_OnReachedSlamDestination()
    {
        enemy.EnemyMovement.OnReachedDestination -= Enemy_OnReachedSlamDestination;

        StartCoroutine(FinishSlam());
    }

    private IEnumerator FinishSlam()
    {
        damageCollider.enabled = false;

        yield return new WaitForSeconds(slamEndDelay);

        Vector2 destinationPosition = BossArena.Instance.GetDestination(endDestination);

        enemy.EnemyMovement.ResetSpeed();
        enemy.EnemyMovement.SetDestination(destinationPosition);
        enemy.EnemyMovement.OnReachedDestination += Enemy_OnReachedEndDestination;

        enemy.EnemyAnimationController.SetAnimatorTrigger(EnemyAnimatorParameter.FinishSlam);

        enemy.EnemyAnimationController.Flip();
    }

    private void Enemy_OnReachedEndDestination()
    {
        enemy.EnemyAnimationController.Flip();

        enemy.EnemyMovement.OnReachedDestination -= Enemy_OnReachedEndDestination;
    }
}
