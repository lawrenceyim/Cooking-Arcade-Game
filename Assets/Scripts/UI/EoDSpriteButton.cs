using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EoDSpriteButton : MonoBehaviour
{
    public enum ButtonAction {
        StartNextDay,
        ReturnToMenu,
        ExitGame
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
        }
    }
}
