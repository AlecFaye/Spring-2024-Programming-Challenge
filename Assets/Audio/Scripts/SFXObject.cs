using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(AudioSource))]
public class SFXObject : MonoBehaviour
{
    private AudioSource audioSource;

    private ObjectPool<SFXObject> pool;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartPlaying(AudioClip clip)
    {
        audioSource.clip = clip;

        StartCoroutine(PlayAudioClip());
    }

    public void SetPool(ObjectPool<SFXObject> pool)
    {
        this.pool = pool;
    }

    private IEnumerator PlayAudioClip()
    {
        WaitForSeconds wait = new(audioSource.clip.length);

        audioSource.Play();

        yield return wait;

        pool.Release(this);
    }
}
