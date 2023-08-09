using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void NewGame() {
        PlayerData.ResetSave();
        PlayerData.LoadData();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Intro");
    }

    public void LoadGame() {
        Time.timeScale = 1f;
        PlayerData.StartDay();
        PlayerData.LoadData();
        SceneManager.LoadScene("MainLevel");
    }

    public void QuitGame() {
        Application.Quit();
    }
    
    public void ReturnToMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
