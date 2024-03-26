using UnityEngine;

[CreateAssetMenu(fileName = "Wave Scriptable Object", menuName = "Scriptable Object/Wave Scriptable Object")]
public class WaveScriptableObject : ScriptableObject
{
    [SerializeField] private Wave[] waves;
    [SerializeField] private float waveEndDelay;
    [SerializeField] private bool isBossWave = false;

    public Wave[] Waves => waves;
    public float WaveEndDelay => waveEndDelay;
    public bool IsBossWave => isBossWave;
}

[System.Serializable]
public struct Wave
{
    public Spawn[] Spawns;
    public float Delay;
}

[System.Serializable]
public struct Spawn
{
    public ObstacleType ObstacleType;
    public SpawnPosition SpawnPosition;
}

public enum SpawnPosition
{
    Top,
    TopMid,
    BotMid,
    Bot,
    LeftTop,
    RightTop
}
