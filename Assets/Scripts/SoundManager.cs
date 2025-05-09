using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance{get; private set;}
    public AudioSource seAudioSource;

    [Header("Audio Clip")]
    public AudioClip shootClip;
    public AudioClip explosionClip;
    public AudioClip rockExplosion;
    public AudioClip deathByRock;
    public AudioSource bgmSource;
    public AudioClip bgm;
    void Awake()
    {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }
    public void PlayShootSE(){
        seAudioSource.PlayOneShot(shootClip,0.4f);
    }

    public void PlayExplosionSE(){
        seAudioSource.PlayOneShot(explosionClip,0.8f);
    }
    public void playRockExplosion(){
        seAudioSource.PlayOneShot(rockExplosion,1.2f);
    }
    public void playDeathByRock(){
        seAudioSource.PlayOneShot(deathByRock,1.3f);
    }
    public void playBGM(AudioClip clip,float volume=0.8f)
    {
        bgmSource.clip = clip;
        bgmSource.volume = volume;
        bgmSource.loop = true;
        bgmSource.Play();
    }
    public void StopBGM(){
        bgmSource.Stop();
    }
}
