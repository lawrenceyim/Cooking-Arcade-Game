using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour, Subject {
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject[] observerGameObjects;
    List<Observer> observers;

    private void Start() {
        observers = new List<Observer>();
        foreach (GameObject gameObject in observerGameObjects) {
            observers.Add(gameObject.GetComponent<Observer>());
        }
        pauseMenu.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameState.IsGameIsPaused()) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }

    public void PauseGame() {
        GameState.PauseGame();
        pauseMenu.SetActive(true);
        UpdateObserver();
    }

    public void ResumeGame() {
        GameState.UnpauseGame();
        pauseMenu.SetActive(false);
        UpdateObserver();
    }

    public void AddObserver()
    {
        throw new System.NotImplementedException();
    }

    public void RemoveObserver()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateObserver()
    {
        foreach (Observer observer in observers) {
            observer.ReceiveUpdate();
        }
    }
}
