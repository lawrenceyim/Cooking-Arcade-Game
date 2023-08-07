using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    bool paused;
    
    private void Start() {
        pauseMenu.SetActive(false);
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
    }

    public void ResumeGame() {
        paused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
}
