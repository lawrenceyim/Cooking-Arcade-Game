using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Slider volumeSlider;
    bool paused;
    
    private void Start() {
        pauseMenu.SetActive(false);
        volumeSlider.value = PlayerData.volumeSetting;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (paused) {
                paused = false;
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
            } else {
                paused = true;
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
            }
        } 
        if (PlayerData.volumeSetting != volumeSlider.value) {
            PlayerData.volumeSetting = volumeSlider.value;    
            ES3.Save("volumeSetting", PlayerData.volumeSetting);
        }
    }

    public void ResumeGame() {
        paused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
}
