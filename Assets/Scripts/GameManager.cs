using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioClip bgm;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.playBGM(bgm,0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
