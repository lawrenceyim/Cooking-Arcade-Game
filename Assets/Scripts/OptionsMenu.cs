using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider soundEffectVolumeSlider;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip sampleSoundEffect;
    [SerializeField] AudioClip sampleMusic;

    void Start() {
        musicVolumeSlider.value = PlayerData.musicVolumeSetting;
        soundEffectVolumeSlider.value = PlayerData.soundEffectVolumeSetting;
    }

    void Update() {
        if (PlayerData.soundEffectVolumeSetting != soundEffectVolumeSlider.value) {
            PlayerData.soundEffectVolumeSetting = soundEffectVolumeSlider.value;
            ES3.Save("soundEffectVolumeSetting", PlayerData.soundEffectVolumeSetting);
            PlaySampleSoundEffect();
        }
        if (PlayerData.musicVolumeSetting != musicVolumeSlider.value) {
            PlayerData.musicVolumeSetting = musicVolumeSlider.value;
            ES3.Save("musicVolumeSetting", PlayerData.musicVolumeSetting);
            PlaySampleMusic();
        }
    }

    private void PlaySampleSoundEffect() {
        audioSource.volume = PlayerData.soundEffectVolumeSetting;
        if (audioSource.clip != sampleSoundEffect) {
            audioSource.clip = sampleSoundEffect;
        }
        if (!audioSource.isPlaying) {
            audioSource.Play();
        }
    }

    private void PlaySampleMusic() {
        audioSource.volume = PlayerData.musicVolumeSetting;
        if (audioSource.clip != sampleMusic) {
            audioSource.clip = sampleMusic;
        }
        if (!audioSource.isPlaying) {
            audioSource.Play();
        }
    }
}
