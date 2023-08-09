using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void NewGame() {
        ES3.DeleteFile("SaveFile.es3");
        ES3.Save("money", 1000);
        ES3.Save("day", 1);
        Time.timeScale = 1f;
        SceneManager.LoadScene("IntroLetter");
    }

    public void LoadGame() {
        Time.timeScale = 1f;
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
