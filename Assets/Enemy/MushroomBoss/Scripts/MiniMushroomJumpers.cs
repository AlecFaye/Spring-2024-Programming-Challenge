using UnityEngine;

public class MiniMushroomJumpers : EnemyAbility
{
    [Header("References")]
    [SerializeField] private Enemy enemy;

    [Header("Mini Mushroom Configurations")]
    [SerializeField] private Destination destination;

    private void Start()
    {
        OnTriggerAbility += TriggerAbility;
    }
}
