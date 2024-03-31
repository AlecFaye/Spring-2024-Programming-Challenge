using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public static TutorialUI Instance { get; private set; }

    public delegate void TutorialUIEvent();
    public TutorialUIEvent OnCompletedTutorial;

    [Header("Tutorial Configurations")]
    [SerializeField] private GameObject tutorialObject;
    [SerializeField] private GameObject[] tutorials;
    [SerializeField] private float tutorialShowTime = 5.0f;
    [SerializeField] private float tutorialShowDelay = 1.0f;

    [Header("Skip Configurations")]
    [SerializeField] private GameObject skipObject;
    [SerializeField] private GameObject skipTextObject;
    [SerializeField] private GameObject skipImageObject;
    [SerializeField] private Image skipImage;
    [SerializeField] private float skipTime = 1.0f;

    private float time = 0.0f;

    private bool isTutorialFinished = false;
    private bool isHoldingDownSkip = false;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("There is more than one instance of Tutorial UI in the scene");

        Instance = this;

        skipImageObject.SetActive(false);
    }

    private void Start()
    {
        DisableTutorials();

        StartCoroutine(StartTutorial());
    }

    private void Update()
    {
        if (isTutorialFinished || !isHoldingDownSkip)
            return;

        time += Time.deltaTime;

        skipImage.fillAmount = (time / skipTime);

        if (time > skipTime)
            FinishTutorial();
    }

    public void OnSkip(InputAction.CallbackContext context)
    {
        if (isTutorialFinished)
            return;

        if (context.started)
        {
            time = 0.0f;
            isHoldingDownSkip = true;
            skipTextObject.SetActive(false);
            skipImageObject.SetActive(true);
        }
        else if (context.canceled)
        {
            isHoldingDownSkip = false;
            skipTextObject.SetActive(true);
            skipImageObject.SetActive(false);
        }
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

        FinishTutorial();
    }

    private void DisableTutorials()
    {
        foreach (GameObject tutorial in tutorials)
            tutorial.SetActive(false);
        tutorialObject.SetActive(false);
    }
    
    private void FinishTutorial()
    {
        isTutorialFinished = true;
        StopAllCoroutines();
        tutorialObject.SetActive(false);
        OnCompletedTutorial?.Invoke();
        skipObject.SetActive(false);
    }
}
