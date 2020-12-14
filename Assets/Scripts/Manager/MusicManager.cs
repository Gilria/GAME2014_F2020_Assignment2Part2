using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoSingleton<MusicManager>
{
    AudioSource ad;
    public AudioClip bg;
    public bool isGameOver = false;

    protected override void Awake()
    {
        base.Awake();
        ad = GetComponent<AudioSource>();
        PlayMusic();
    }

    public void PlayMusic()
    {
        ad.clip = bg;
        ad.Play();
    }


    private void Update()
    {
        if (isGameOver)
        {
            if(ad.pitch > 0.4f)
            {
                ad.pitch -= Time.deltaTime * 0.5f;
            }
            else 
            {
                isGameOver = false;
            }
        }
    }

}
