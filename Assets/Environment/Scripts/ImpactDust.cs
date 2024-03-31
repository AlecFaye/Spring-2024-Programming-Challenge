using UnityEngine;

public class ImpactDust : MonoBehaviour
{
    [SerializeField] private GameObject parentObject;

    private void Awake()
    {
        parentObject.SetActive(false);
    }

    public void OnFinishAnimation()
    {
        parentObject.SetActive(false);
    }
}
