using UnityEngine;

public class LineFireballAbility : EnemyAbility
{
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private SpawnPosition spawnIndex;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TriggerAbility();
        }
    }

    public override void TriggerAbility()
    {
        Transform spawnTF = Spawner.Instance.SpawnerPositions[(int)spawnIndex];

        Instantiate(fireballPrefab, spawnTF.position, Quaternion.identity);
    }
}
