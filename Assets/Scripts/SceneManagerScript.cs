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
        SceneManager.LoadScene("Intro");
    }

    public void LoadGame() {
        PlayerData.LoadData();
        GameState.UnpauseGame();
        if (PlayerData.day == 8 && !pizzaLetterIntroPlayed) {
            ES3.Save(Es3Values.PIZZA_LETTER_PLAYED, true);
            SceneManager.LoadScene("PizzaLetter");
            return;
        }
        if (PlayerData.day == 15 && !saladLetterIntroPlayed) {
            ES3.Save(Es3Values.SALAD_LETTER_PLAYED, true);
            SceneManager.LoadScene("SaladLetter");
            return;
        }
        PlayerData.StartDay();
        SceneManager.LoadScene("MainLevel");
    }

    public void StartNextDay() {
        LoadGame();
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ReturnToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadPizzaLetter() {
        SceneManager.LoadScene("PizzaLetter");
    }

    public void LoadSaladLetter() {
        SceneManager.LoadScene("SaladLetter");
    }

    public void LoadOptions() {
        SceneManager.LoadScene("Options");
    }
}
