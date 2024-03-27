using UnityEngine;
using UnityEngine.SceneManagement;

public class EoDSpriteButton : MonoBehaviour
{
    public enum ButtonAction {
        StartNextDay = 0,
        ReturnToMenu = 1,
        ExitGame = 2,
        EndGame = 3,
        Retry = 4,
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
            case ButtonAction.Retry:
                PlayerData.DecrementDay();
                PlayerData.SaveData();
                sceneManagerScript.LoadGame();
                break;
        }
    }

    public void SetButtonAction(ButtonAction buttonAction) {
        this.buttonAction = buttonAction;
    }
}
