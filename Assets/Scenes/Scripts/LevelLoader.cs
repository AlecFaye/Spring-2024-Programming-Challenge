using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName
{
    Game,
    MainMenu,
}

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(SceneName sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }
}
