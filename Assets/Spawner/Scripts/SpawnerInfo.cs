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

    [SerializeField] private Transform spawnPositionsTF;
    [SerializeField] private float hardModePercent = 0.5f;
    [SerializeField] private float difficultyTime = 10 * 60;

    private Transform[] spawnerPositions;
    public Transform[] SpawnerPositions => spawnerPositions;

    public bool IsHardModeOn { get; private set; }
    public float DifficultyTime => difficultyTime;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("There is more than one Spawner Info in the scene.");

        Instance = this;

        InitSpawnPositions();
    }

    private void Update()
    {
        IsHardModeOn = (GameTimer.Instance.CurrentTimeInSeconds / difficultyTime) > hardModePercent;
    }

    private void InitSpawnPositions()
    {
        spawnerPositions = new Transform[spawnPositionsTF.childCount];
        for (int index = 0; index < spawnPositionsTF.childCount; index++)
            spawnerPositions[index] = spawnPositionsTF.GetChild(index).transform;
    }
}
