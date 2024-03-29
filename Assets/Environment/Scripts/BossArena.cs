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
    Hover,
    Slam,

    EntranceDarkKnight,
    Laser,
    ParryProjectile,
    ParryProjectileSpawn,
    PlatformCreator,
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
    [SerializeField] private Transform hoverTF;
    [SerializeField] private Transform slamTF;

    [Header("Dark Knight Configurations")]
    [SerializeField] private Transform entranceDarkKnightTF;
    [SerializeField] private Transform laserTF;
    [SerializeField] private Transform parryProjectileTF;
    [SerializeField] private Transform parryProjectileSpawnTF;
    [SerializeField] private Transform platformCreatorTF;
    [SerializeField] private GameObject laserHorizontalGO;
    [SerializeField] private GameObject laserVerticalLeftGO;
    [SerializeField] private GameObject laserVerticalRightGO;

    private readonly Dictionary<Destination, Vector2> destinations = new();
    private readonly Dictionary<LaserType, GameObject> lasers = new();

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("More than one Boss Arena in this scene.");

        Instance = this;

        InitDestinations();
        InitLasers();
    }

    private void InitDestinations()
    {
        destinations.Add(Destination.EntranceFireMage, entranceFireMageTF.position);
        destinations.Add(Destination.LineFireball, lineFireballTF.position);
        destinations.Add(Destination.Firestorm, firestormTF.position);
        destinations.Add(Destination.Meteor, meteorFireballTF.position);

        destinations.Add(Destination.EntranceMushroom, entranceMushroomTF.position);
        destinations.Add(Destination.RollAttackStart, rollAttackStartTF.position);
        destinations.Add(Destination.RollAttack, rollAttackTF.position);
        destinations.Add(Destination.MiniMushroom, miniMushroomTF.position);
        destinations.Add(Destination.Hover, hoverTF.position);
        destinations.Add(Destination.Slam, slamTF.position);

        destinations.Add(Destination.EntranceDarkKnight, entranceDarkKnightTF.position);
        destinations.Add(Destination.Laser, laserTF.position);
        destinations.Add(Destination.ParryProjectile, parryProjectileTF.position);
        destinations.Add(Destination.ParryProjectileSpawn, parryProjectileSpawnTF.position);
        destinations.Add(Destination.PlatformCreator, platformCreatorTF.position);
    }

    private void InitLasers()
    {
        lasers.Add(LaserType.Horizontal, laserHorizontalGO);
        lasers.Add(LaserType.VerticalLeft, laserVerticalLeftGO);
        lasers.Add(LaserType.VerticalRight, laserVerticalRightGO);
    }

    public Vector2 GetDestination(Destination destination)
    {
        if (destinations.TryGetValue(destination, out Vector2 destinationPosition))
        {
            return destinationPosition;
        }
        else
        {
            Debug.LogError($"There is no destination of type: {destination}. Returning destination: (0, 0)");
            return Vector2.zero;
        }
    }

    public GameObject GetLaser(LaserType laserType)
    {
        if (lasers.TryGetValue(laserType, out GameObject laser))
        {
            return laser;
        }
        else
        {
            Debug.LogError($"There is no laser of type: {laser}. Returning null");
            return null;
        }
    }
}
