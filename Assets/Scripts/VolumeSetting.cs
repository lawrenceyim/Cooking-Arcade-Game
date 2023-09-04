using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] bool isMusic;
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        if (isMusic) {
            audioSource.volume = PlayerData.musicVolumeSetting;        
        } else {
            audioSource.volume = PlayerData.soundEffectVolumeSetting;        
        }
    }

    void Update()
    {
        if (isMusic) {
            audioSource.volume = PlayerData.musicVolumeSetting;        
        } else {
            audioSource.volume = PlayerData.soundEffectVolumeSetting;        
        }
    }
}
