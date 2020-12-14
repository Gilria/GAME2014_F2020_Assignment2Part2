﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManageer : MonoSingleton<SoundManageer>
{
    public AudioClip[] sfx;

    AudioSource ad;

    protected override void Awake()
    {
        base.Awake();
        ad = GetComponent<AudioSource>();
    }

    public void PlaySFX(int id)
    {
        ad.PlayOneShot(sfx[id]);
    }

    public void StopSFX()
    {
        ad.Stop();
    }
}
