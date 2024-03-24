using UnityEngine;
using UnityEngine.SceneManagement;

public class EoDSpriteButton : MonoBehaviour
{
    public enum ButtonAction {
        StartNextDay,
        ReturnToMenu,
        ExitGame,
        EndGame,
    }

    [SerializeField] ButtonAction buttonAction;
    [SerializeField] SceneManagerScript sceneManagerScript;

    private void OnMouseDown() {
        switch(buttonAction) {
            case ButtonAction.StartNextDay:
                sceneManagerScript.LoadGame();
                break;
            case ButtonAction.ReturnToMenu:
                SceneManager.LoadScene("MainMenu");
                break;
            case ButtonAction.ExitGame:
                Application.Quit();
                break;
            case ButtonAction.EndGame:
                sceneManagerScript.LoadEndLetter();
                break;
        }
    }

    public void SetButtonAction(ButtonAction buttonAction) {
        this.buttonAction = buttonAction;
    }
}
