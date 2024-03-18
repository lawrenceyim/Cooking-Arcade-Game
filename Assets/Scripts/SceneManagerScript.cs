using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour {
    bool pizzaLetterIntroPlayed;
    bool saladLetterIntroPlayed;

    private void Start() {
        pizzaLetterIntroPlayed = ES3.Load(Es3Values.PIZZA_LETTER_PLAYED, false);
        saladLetterIntroPlayed = ES3.Load(Es3Values.SALAD_LETTER_PLAYED, false);
    }

    public void NewGame() {
        PlayerData.ResetSave();
        PlayerData.LoadData();
        GameState.UnpauseGame();
        SceneManager.LoadScene(SceneName.INTRO.name);
    }

    public void LoadGame() {
        PlayerData.LoadData();
        GameState.UnpauseGame();
        if (PlayerData.day == 8 && !pizzaLetterIntroPlayed) {
            ES3.Save(Es3Values.PIZZA_LETTER_PLAYED, true);
            SceneManager.LoadScene(SceneName.PIZZA_LETTER.name);
            return;
        }
        if (PlayerData.day == 15 && !saladLetterIntroPlayed) {
            ES3.Save(Es3Values.SALAD_LETTER_PLAYED, true);
            SceneManager.LoadScene(SceneName.SALAD_LETTER.name);
            return;
        }
        if (PlayerData.day > 21) {
            PlayerData.ResetSave();
            LoadEndLetter();
        }
        PlayerData.StartDay();
        SceneManager.LoadScene(SceneName.MAIN_LEVEL.name);
    }

    public void StartNextDay() {
        LoadGame();
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ReturnToMainMenu() {
        SceneManager.LoadScene(SceneName.MAIN_MENU.name);
    }

    public void LoadPizzaLetter() {
        SceneManager.LoadScene(SceneName.PIZZA_LETTER.name);
    }

    public void LoadSaladLetter() {
        SceneManager.LoadScene(SceneName.SALAD_LETTER.name);
    }

    public void LoadOptions() {
        SceneManager.LoadScene(SceneName.OPTIONS.name);
    }

    public void OpenTutorialScene() {
        SceneManager.LoadScene(SceneName.TUTORIAL.name);
    }

    public void LoadEndLetter() {
        SceneManager.LoadScene(SceneName.END_LETTER.name);
    }
}
