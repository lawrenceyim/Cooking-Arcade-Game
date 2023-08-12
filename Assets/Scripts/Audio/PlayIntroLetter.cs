using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayIntroLetter : MonoBehaviour
{
    [SerializeField] AudioClip partA;
    [SerializeField] AudioClip partB;
    [SerializeField] AudioSource audioSource;
    double initLatency = .1d;

    private bool isPlayingFirstClip = true;
    private bool hasPlayedSecondClip = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayFirstClip();
    }

    private void PlayFirstClip()
    {
        audioSource.clip = partA;
        audioSource.Play();
    }

    private void PlaySecondClip()
    {
        audioSource.clip = partB;
        audioSource.Play();
    }

    private void Update()
    {
        if (hasPlayedSecondClip) return;

        if (!audioSource.isPlaying)
        {
            if (isPlayingFirstClip)
            {
                PlaySecondClip();
                hasPlayedSecondClip = true;
            }
            else
            {
                PlayFirstClip();
            }
            isPlayingFirstClip = !isPlayingFirstClip;
        }
    }
}
