using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public AudioSource seAudioSource;

    [Header("Audio Clip")]
    public AudioClip shootClip;
    public AudioClip explosionClip;
    public AudioClip rockExplosion;
    public AudioClip deathByRock;
    public AudioClip roboShoot;
    public AudioClip roboClash;
    public AudioClip roboWalk;
    public AudioSource roboWalkSource;
    public AudioSource bgmSource;
    public AudioClip bgm;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void PlayShootSE()
    {
        seAudioSource.PlayOneShot(shootClip, 0.5f);
    }

    public void PlayExplosionSE()
    {
        seAudioSource.PlayOneShot(explosionClip, 0.8f);
    }
    public void playRockExplosion()
    {
        seAudioSource.PlayOneShot(rockExplosion, 1.2f);
    }
    public void playDeathByRock()
    {
        seAudioSource.PlayOneShot(deathByRock, 1.3f);
    }
    public void PlayRoboWalkLoop()
    {
        // if (seAudioSource.isPlaying) return;
        roboWalkSource.clip = roboWalk;
        roboWalkSource.volume = 0.5f;
        roboWalkSource.loop = true;
        roboWalkSource.Play();
    }
    public void StopRoboWalk()
    {
        if (roboWalkSource.isPlaying)
        {
            roboWalkSource.Stop();
            roboWalkSource.loop = false;
            //seAudioSource.clip = null;
        }
    }
    public void PlayRoboShoot()
    {
        seAudioSource.PlayOneShot(roboShoot,0.8f);
    }
    public void PlayRoboClash()
    {
        seAudioSource.PlayOneShot(roboClash,0.6f);
    }
    public void playBGM(AudioClip clip, float volume = 0.8f)
    {
        bgmSource.clip = clip;
        bgmSource.volume = volume;
        bgmSource.loop = true;
        bgmSource.Play();
    }
    public void StopBGM()
    {
        bgmSource.Stop();
    }
}
