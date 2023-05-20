using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{

    AudioSource audioSource;
    public AudioClip[] Clips;
    private AudioClip music;
    bool playMusic;
    bool changeMusic;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        changeMusic = true;
        playMusic = true;
        
    }

    
    void Update()
    {
        if(playMusic && changeMusic)
        {
            music = Clips[Random.Range(0, Clips.Length)];
            audioSource.clip = music;
            audioSource.Play();
            changeMusic = false;
        }
        if(music.length == audioSource.time)
        {
            audioSource.Stop();
            changeMusic = true;
        }


    }
}
