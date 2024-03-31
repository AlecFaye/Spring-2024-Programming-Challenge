using System.Collections;
using TMPro;
using UnityEngine;

public class DifficultyUI : MonoBehaviour
{
    private const float UPDATE_RATE = 0.1f;

    [SerializeField] private TextMeshProUGUI difficultyText;

    private void Start()
    {
        StartCoroutine(UpdateDifficultyUI());
    }

    private IEnumerator UpdateDifficultyUI()
    {
        WaitForSeconds wait = new(UPDATE_RATE);

        while (!SpawnerInfo.Instance.IsHardModeOn)
            yield return wait;

        difficultyText.text = "HARD";
    }
}
