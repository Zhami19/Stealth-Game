using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip investigating, winning, losing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void InvestigatingSound()
    {
        audioSource.PlayOneShot(investigating);
    }
    public void WinningSound()
    {
        audioSource.PlayOneShot(winning);
    }
    public void LosingSound()
    {
        audioSource.PlayOneShot(losing);
    }

    public void SneakingVolume()
    {
        audioSource.volume /= 2;
    }

    public void NormalVolume()
    {
        audioSource.volume = 1;
    }
}