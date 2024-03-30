using System.Collections;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public static TutorialUI Instance { get; private set; }

    public delegate void TutorialUIEvent();
    public TutorialUIEvent OnCompletedTutorial;

    [SerializeField] private GameObject tutorialObject;
    [SerializeField] private GameObject[] tutorials;
    [SerializeField] private float tutorialShowTime = 5.0f;
    [SerializeField] private float tutorialShowDelay = 1.0f;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("There is more than one instance of Tutorial UI in the scene");

        Instance = this;
    }

    private void Start()
    {
        DisableTutorials();

        StartCoroutine(StartTutorial());
    }

    private IEnumerator StartTutorial()
    {
        WaitForSeconds showTime = new(tutorialShowTime);
        WaitForSeconds showDelay = new(tutorialShowDelay);

        foreach (GameObject tutorial in tutorials)
        {
            yield return showDelay;
            tutorialObject.SetActive(true);
            tutorial.SetActive(true);

            yield return showTime;
            tutorial.SetActive(false);
            tutorialObject.SetActive(false);
        }

        OnCompletedTutorial?.Invoke();
    }

    private void DisableTutorials()
    {
        foreach (GameObject tutorial in tutorials)
            tutorial.SetActive(false);
        tutorialObject.SetActive(false);
    }
}
