using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    [SerializeField] GameObject pauseMenu;

    private void Start() {
        pauseMenu.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameState.gameIsPaused()) {
                Time.timeScale = 1f;
                GameState.unpauseGame();
                pauseMenu.SetActive(false);
            } else {
                Time.timeScale = 0f;
                GameState.pauseGame();
                pauseMenu.SetActive(true);
            }
        }
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
}
