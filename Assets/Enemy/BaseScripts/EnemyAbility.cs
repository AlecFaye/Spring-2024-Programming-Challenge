using UnityEngine;

public class EnemyAbility : MonoBehaviour
{
    public delegate void AbilityEvent();
    public AbilityEvent OnStartAbility;
    public AbilityEvent OnTriggerAbility;
    public AbilityEvent OnEndAbility;

    public bool IsUsingAbility { get; private set; }

    [SerializeField] private float abilityCooldown;
    [SerializeField] private float abilityAftermathCooldown;

    public float AbilityAftermathCooldown => abilityAftermathCooldown;

    private float abilityTime = -999f;

    private void Start()
    {
        IsUsingAbility = false;
        OnEndAbility += Ability_OnEndAbility;
    }

    public virtual bool CanUseAbility()
    {
        return abilityTime + abilityCooldown <= Time.time;
    }

    public virtual void StartAbility()
    {
        abilityTime = Time.time;

        IsUsingAbility = true;

        OnStartAbility?.Invoke();
    }

    public virtual void TriggerAbility() { }

    private void Ability_OnEndAbility()
    {
        IsUsingAbility = false;
    }
}
