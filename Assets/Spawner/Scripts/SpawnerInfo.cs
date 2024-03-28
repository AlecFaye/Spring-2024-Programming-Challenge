using UnityEngine;

public enum SpawnPosition
{
    Top,
    TopMid,
    BotMid,
    Bot,
    LeftTop,
    RightTop
}

public class SpawnerInfo : MonoBehaviour
{
    public static SpawnerInfo Instance {  get; private set; }
    public static float DifficultyTime = 1 * 30;

    [SerializeField] private Transform spawnPositionsTF;

    private Transform[] spawnerPositions;
    public Transform[] SpawnerPositions => spawnerPositions;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("There is more than one Spawner Info in the scene.");

        Instance = this;

        InitSpawnPositions();
    }

    private void InitSpawnPositions()
    {
        spawnerPositions = new Transform[spawnPositionsTF.childCount];
        for (int index = 0; index < spawnPositionsTF.childCount; index++)
            spawnerPositions[index] = spawnPositionsTF.GetChild(index).transform;
    }
}
