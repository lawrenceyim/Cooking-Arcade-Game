using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    
    private void Start() {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Time.timeScale == 0f) {
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
            } else {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
            }
        } 
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
}
