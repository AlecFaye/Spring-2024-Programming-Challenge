using UnityEngine;

public class EnemyAbility : MonoBehaviour
{
    public delegate void AbilityEvent();
    public AbilityEvent OnUseAbility;

    [HideInInspector] public bool IsUsingAbility;

    [SerializeField] private float abilityCooldown;

    private float abilityTime = -999f;

    public virtual bool CanUseAbility()
    {
        return abilityTime + abilityCooldown <= Time.time;
    }

    public virtual void StartAbility()
    {
        abilityTime = Time.time;

        IsUsingAbility = true;

        OnUseAbility?.Invoke();
    }

    public virtual void TriggerAbility() { }
}
