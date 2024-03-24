using UnityEngine;

public class DangerIndicatorManager : MonoBehaviour
{
    public static DangerIndicatorManager Instance { get; private set; }

    private GameObject[] indicators;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("There is more than one Danger Indicator Manager in the scene.");

        Instance = this;

        InitIndicators();
    }

    public void DisplayIndicator(SpawnPosition spawnPosition)
    {
        if ((int)spawnPosition >= indicators.Length)
        {
            Debug.LogError("Not enough indicators.");
            return;
        }

        GameObject indicator = indicators[(int)spawnPosition];
        if (!indicator.activeInHierarchy)
            indicator.SetActive(true);
    }

    private void InitIndicators()
    {
        indicators = new GameObject[transform.childCount];

        for (int index = 0; index < indicators.Length; index++)
            indicators[index] = transform.GetChild(index).gameObject;
    }
}
