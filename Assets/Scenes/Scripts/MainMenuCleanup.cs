using UnityEngine;

public class MainMenuCleanup : MonoBehaviour
{
    private void Awake()
    {
        if (LevelLoader.Instance != null)
            Destroy(LevelLoader.Instance.gameObject);
    }
}
