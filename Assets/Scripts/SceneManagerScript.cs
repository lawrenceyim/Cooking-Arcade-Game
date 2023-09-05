using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    bool pizzaLetterIntroPlayed;

    private void Start() {
        pizzaLetterIntroPlayed = ES3.Load("pizzaLetterIntroPlayed", false);
    }

    public void NewGame() {
        PlayerData.ResetSave();
        PlayerData.LoadData();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Intro");
    }

    public void LoadGame() {
        Time.timeScale = 1f;
        if (PlayerData.day == 8 && !pizzaLetterIntroPlayed) {
            ES3.Save("pizzaLetterIntroPlayed", true);
            SceneManager.LoadScene("PizzaLetter");
            return;
        }
        PlayerData.StartDay();
        PlayerData.LoadData();
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

    public void LoadOptions() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Options");
    }
}
