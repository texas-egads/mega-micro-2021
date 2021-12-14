using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAlertHandler : MonoBehaviour
{
    [SerializeField] private Animator boss;
    [SerializeField] private AudioSource source;
    
    public void BossAlert()
    {
        FindObjectOfType<AudioManager>()._source.Stop();
        source.Play();
        boss.Play("boss-alert-scroll");
    }
}
