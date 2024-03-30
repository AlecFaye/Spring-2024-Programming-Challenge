using System.Collections.Generic;
using UnityEngine;

public enum BGMType
{
    Regular,
    FireMage,
    Mushroom,
    DarkKnight,
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Music Configurations")]
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioClip regularBGM;
    [SerializeField] private AudioClip fireMageBGM;
    [SerializeField] private AudioClip mushroomBGM;
    [SerializeField] private AudioClip darkKnightBGM;

    [Header("SFX Configurations")]
    [SerializeField] private SFXObjectPool sfxObjectPool;

    private readonly Dictionary<BGMType, AudioClip> backgroundMusic = new();

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("More than one instance of Audio Manager in this scene.");

        Instance = this;
    }

    private void Start()
    {
        InitBGM();

        PlayBGM(BGMType.Regular);
    }

    public void PlayBGM(BGMType bgmType)
    {
        if (backgroundMusic.TryGetValue(bgmType, out AudioClip audioClip))
            musicAudioSource.clip = audioClip;
        else
            Debug.LogError($"There is no BGM Type of {bgmType}.");

        musicAudioSource.Play();
    }

    public void PlayRegularBGM()
    {
        PlayBGM(BGMType.Regular);
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxObjectPool.Pool.Get(out SFXObject sfxObject);
        sfxObject.StartPlaying(clip);
    }

    private void InitBGM()
    {
        backgroundMusic.Add(BGMType.Regular, regularBGM);
        backgroundMusic.Add(BGMType.FireMage, fireMageBGM);
        backgroundMusic.Add(BGMType.Mushroom, mushroomBGM);
        backgroundMusic.Add(BGMType.DarkKnight, darkKnightBGM);
    }
}
