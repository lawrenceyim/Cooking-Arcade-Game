using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpriteButton : MonoBehaviour {
    [SerializeField] GameObject pauseMenu;
    [SerializeField] string functionName;
    private void OnMouseDown() {
        if (functionName == "Return") {
            ReturnToMainMenu();
        } else if (functionName == "Unpause") {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        } else if (functionName == "Exit") {
            Application.Quit();
        }
    }

    public void ReturnToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
