
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;
    
    [SerializeField] private AudioSource musicSource;
    [SerializeField]private AudioSource soundSource;

    [SerializeField] public AudioClip[] moneySound;
    [SerializeField] public AudioClip baseDamageSound;

    [SerializeField] private AudioClip[] musics;
    [SerializeField] public AudioClip endMusic;
    
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

    private void Update()
    {
        if (!musicSource.isPlaying)
        {
            ChangeMusic(Random.Range(0,musics.Length));
        }
    }
}
