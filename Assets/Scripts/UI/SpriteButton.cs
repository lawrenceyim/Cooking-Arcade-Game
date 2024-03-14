using UnityEngine;
using UnityEngine.SceneManagement;

public class SpriteButton : MonoBehaviour {
    [SerializeField] GameObject pauseMenu;
    [SerializeField] string functionName;
    [SerializeField] PauseMenu pauseMenuScript;

    private void OnMouseDown() {
        if (functionName == "Return") {
            ReturnToMainMenu();
        } else if (functionName == "Unpause") {
            pauseMenuScript.ResumeGame();
            pauseMenu.SetActive(false);
        } else if (functionName == "Exit") {
            Application.Quit();
        }
    }

    public void ReturnToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
