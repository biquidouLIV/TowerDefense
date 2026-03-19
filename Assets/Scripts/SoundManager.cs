
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;
    
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;

    [SerializeField] public AudioClip[] moneySound;
    [SerializeField] public AudioClip[] baseDamageSound;

    [SerializeField] private AudioClip[] musics;
    [SerializeField] public AudioClip endMusic;

    [SerializeField] public AudioClip shootSound;
    private bool playMusic = true;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    

    public void RequestPlaySound(AudioClip sound, float volume = 1.0f)
    {
        soundSource.PlayOneShot(sound, volume);
    }

    public void ChangeMusic(int music)
    {
        musicSource.Stop();
        musicSource.clip = musics[music];
        musicSource.Play();
    }

    public void EndMusic()
    {
        playMusic = false;
        musicSource.Stop();
        RequestPlaySound(endMusic);
    }
    
    private void Update()
    {
        if (!musicSource.isPlaying && playMusic)
        {
            ChangeMusic(Random.Range(0,musics.Length));
        }
    }
}
