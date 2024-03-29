using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : EnemyAbility
{
    [Header("References")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private Destination destination;

    [Header("Laser Attack Configurations")]
    [SerializeField] private float chargeAttackTime = 1.0f;

    private readonly Dictionary<LaserType, SpawnPosition> dangerIndicators = new()
    {
        { LaserType.Horizontal, SpawnPosition.Bot },
        { LaserType.VerticalLeft, SpawnPosition.LeftTop },
        { LaserType.VerticalRight, SpawnPosition.RightTop },
    };

    private LaserType laserType;
    private GameObject laser;

    private void Start()
    {
        OnTriggerAbility += TriggerAbility;
    }

    public override void StartAbility()
    {
        base.StartAbility();

        Move();
        InitLaser();
    }

    public override void TriggerAbility()
    {
        laser.SetActive(true);
    }

    private void Move()
    {
        Vector2 destinationPosition = BossArena.Instance.GetDestination(destination);
        enemy.EnemyMovement.SetDestination(destinationPosition);
        enemy.EnemyMovement.OnReachedDestination += Enemy_OnReachedDestination;
    }

    private void InitLaser()
    {
        if (enemy.EnemyAI.IsHardModeOn)
            laserType = ChooseRandomLaser();
        else
            laserType = LaserType.Horizontal;

        laser = BossArena.Instance.GetLaser(laserType);
        if (dangerIndicators.TryGetValue(laserType, out SpawnPosition spawnPosition))
            DangerIndicatorManager.Instance.DisplayIndicator(spawnPosition);
    }

    private void Enemy_OnReachedDestination()
    {
        enemy.EnemyMovement.OnReachedDestination -= Enemy_OnReachedDestination;

        enemy.EnemyAnimationController.SetAnimatorTrigger(EnemyAnimatorParameter.ChargeFlameAttack);

        StartCoroutine(StartLaserAttack());
    }

    private IEnumerator StartLaserAttack()
    {
        yield return new WaitForSeconds(chargeAttackTime);

        enemy.EnemyAnimationController.SetAnimatorTrigger(EnemyAnimatorParameter.FlameAttack);
    }

    private LaserType ChooseRandomLaser()
    {
        int randomIndex = Random.Range(0, System.Enum.GetValues(typeof(LaserType)).Length);
        return (LaserType)randomIndex;
    }
}
