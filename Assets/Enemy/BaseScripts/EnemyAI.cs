using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private EnemyAbility[] enemyAbilities;

    private EnemyAbility currentAbility;

    private bool isAttacking = false;

    private void Start()
    {
        enemy.EnemyAnimationController.OnTriggerAbility += EnemyAnimator_OnTriggerAbility;
        enemy.EnemyAnimationController.OnEndAbility += EnemyAnimator_OnEndAbility;
    }

    private void Update()
    {
        if (isAttacking || enemy.EnemyStats.IsDead)
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
        }
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
