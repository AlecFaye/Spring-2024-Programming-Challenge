using System.Collections.Generic;
using UnityEngine;

public enum Destination
{
    EntranceFireMage,
    LineFireball,
    Firestorm,
    Meteor,

    EntranceMushroom,
    RollAttackStart,
    RollAttack,
    MiniMushroom,
}

public class BossArena : MonoBehaviour
{
    public static BossArena Instance { get; private set; }

    [Header("Fire Mage Arena Configurations")]
    [SerializeField] private Transform entranceFireMageTF;
    [SerializeField] private Transform lineFireballTF;
    [SerializeField] private Transform firestormTF;
    [SerializeField] private Transform meteorFireballTF;

    [Header("Mushroom Arena Configurations")]
    [SerializeField] private Transform entranceMushroomTF;
    [SerializeField] private Transform rollAttackStartTF;
    [SerializeField] private Transform rollAttackTF;
    [SerializeField] private Transform miniMushroomTF;

    private readonly Dictionary<Destination, Vector2> destinations = new();

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("More than one Boss Arena in this scene.");

        Instance = this;

        destinations.Add(Destination.EntranceFireMage, entranceFireMageTF.position);
        destinations.Add(Destination.LineFireball, lineFireballTF.position);
        destinations.Add(Destination.Firestorm, firestormTF.position);
        destinations.Add(Destination.Meteor, meteorFireballTF.position);

        destinations.Add(Destination.EntranceMushroom, entranceMushroomTF.position);
        destinations.Add(Destination.RollAttackStart, rollAttackStartTF.position);
        destinations.Add(Destination.RollAttack, rollAttackTF.position);
        destinations.Add(Destination.MiniMushroom, miniMushroomTF.position);
    }

    public Vector2 GetDestination(Destination destination)
    {
        if (destinations.TryGetValue(destination, out var destinationPosition))
        {
            return destinationPosition;
        }
        else
        {
            Debug.LogError($"There is no destination of type: {destination}. Returning destination: (0, 0)");
            return Vector2.zero;
        }
    }
}
