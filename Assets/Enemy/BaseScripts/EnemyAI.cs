using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private EnemyAbility[] enemyAbilities;
    [SerializeField] private Destination entranceDestination;
    [SerializeField] private float delayStart = 1.0f;

    private EnemyAbility currentAbility;

    private bool isCurrentlyEntering = true;
    private bool isAttacking = false;
    private bool isCoolingDown = false;

    private void Start()
    {
        enemy.EnemyAnimationController.OnTriggerAbility += EnemyAnimator_OnTriggerAbility;
        enemy.EnemyAnimationController.OnEndAbility += EnemyAnimator_OnEndAbility;

        Vector2 entrancePosition = BossArena.Instance.GetDestination(entranceDestination);
        enemy.EnemyMovement.SetDestination(entrancePosition);
        enemy.EnemyMovement.OnReachedDestination += Enemy_ReachedEntranceDestination;
    }

    private void Enemy_ReachedEntranceDestination()
    {
        enemy.EnemyMovement.OnReachedDestination -= Enemy_ReachedEntranceDestination;

        StartCoroutine(DelayStart());
    }

    private void Update()
    {
        if (isCurrentlyEntering || isAttacking || isCoolingDown || enemy.EnemyStats.IsDead)
            return;

        currentAbility = ChooseAbility();
        UseAbility();
    }

    private EnemyAbility ChooseAbility()
    {
        List<EnemyAbility> availableAbilities = new();

        foreach (EnemyAbility ability in enemyAbilities)
        {
            if (ability.CanUseAbility())
                availableAbilities.Add(ability);
        }

        if (availableAbilities.Count > 0)
        {
            int randomIndex = Random.Range(0, availableAbilities.Count);
            return availableAbilities[randomIndex];
        }

        return null;
    }

    private void UseAbility()
    {
        if (currentAbility != null)
        {
            currentAbility.StartAbility();
            isAttacking = true;
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        isCoolingDown = true;

        yield return new WaitForSeconds(currentAbility.AbilityAftermathCooldown);

        isCoolingDown = false;
    }

    private IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(delayStart);

        isCurrentlyEntering = false;
    }

    private void EnemyAnimator_OnTriggerAbility()
    {
        currentAbility.OnTriggerAbility?.Invoke();
    }

    private void EnemyAnimator_OnEndAbility()
    {
        isAttacking = false;

        currentAbility.OnEndAbility?.Invoke();
    }
}
