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
            audioSource.volume = PlayerData.volumeSetting / 5;        
        } else {
            audioSource.volume = PlayerData.volumeSetting;        
        }
    }

    void Update()
    {
        if (audioSource.volume == PlayerData.volumeSetting) return;
        if (isMusic) {
            audioSource.volume = PlayerData.volumeSetting / 2;        
        } else {
            audioSource.volume = PlayerData.volumeSetting;        
        }  
    }
}
