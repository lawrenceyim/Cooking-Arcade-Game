using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider soundEffectVolumeSlider;

    void Start() {
        musicVolumeSlider.value = PlayerData.musicVolumeSetting;
        soundEffectVolumeSlider.value = PlayerData.soundEffectVolumeSetting;    
    }

    void Update()
    {
        if (PlayerData.soundEffectVolumeSetting != soundEffectVolumeSlider.value) {
            PlayerData.soundEffectVolumeSetting = soundEffectVolumeSlider.value;
            ES3.Save("soundEffectVolumeSetting", PlayerData.soundEffectVolumeSetting);
            // Play example sound effect
        }
        if (PlayerData.musicVolumeSetting != musicVolumeSlider.value) {
            PlayerData.musicVolumeSetting = musicVolumeSlider.value;
            ES3.Save("musicVolumeSetting", PlayerData.musicVolumeSetting);
            // Play example music in background and adjust sound
        }
    }
}
