using UnityEngine;
using UnityEngine.Pool;

public class SFXObjectPool : MonoBehaviour
{
    public ObjectPool<SFXObject> Pool;

    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxSize = 10000;

    public SFXObject SFXObjectPrefab;

    private GameObject parent;

    private void Start()
    {
        parent = new($"{SFXObjectPrefab.name} (Object Pool)");
        Pool = new(CreateSFX, OnGetSFXFromPool, OnReleaseSFXToPool, OnDestroySFX, true, defaultCapacity, maxSize);
    }

    private SFXObject CreateSFX()
    {
        SFXObject sfxObject = Instantiate(SFXObjectPrefab, Vector3.zero, Quaternion.identity, parent.transform);

        sfxObject.SetPool(Pool);

        return sfxObject;
    }

    private void OnGetSFXFromPool(SFXObject sfxObject)
    {
        sfxObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        sfxObject.gameObject.SetActive(true);
    }

    private void OnReleaseSFXToPool(SFXObject sfxObject)
    {
        sfxObject.gameObject.SetActive(false);
    }

    private void OnDestroySFX(SFXObject sfxObject)
    {
        Destroy(sfxObject.gameObject);
    }
}
