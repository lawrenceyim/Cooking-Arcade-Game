using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    bool pizzaLetterIntroPlayed;
    bool saladLetterIntroPlayed;

    private void Start() {
        pizzaLetterIntroPlayed = ES3.Load("pizzaLetterIntroPlayed", false);
        saladLetterIntroPlayed = ES3.Load("saladLetterIntroPlayed", false);
    }

    public void NewGame() {
        PlayerData.ResetSave();
        PlayerData.LoadData();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Intro");
    }

    public void LoadGame() {
        PlayerData.LoadData();
        Time.timeScale = 1f;
        if (PlayerData.day == 8 && !pizzaLetterIntroPlayed) {
            ES3.Save("pizzaLetterIntroPlayed", true);
            SceneManager.LoadScene("PizzaLetter");
            return;
        }
        if (PlayerData.day == 15 && !saladLetterIntroPlayed) {
            ES3.Save("saladLetterIntroPlayed", true);
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
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadPizzaLetter() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("PizzaLetter");
    }

    public void LoadSaladLetter() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SaladLetter");
    }

    public void LoadOptions() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Options");
    }
}
