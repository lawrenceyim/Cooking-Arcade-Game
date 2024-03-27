using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataUI : MonoBehaviour, Subject {
    [SerializeField] Controller controller;
    [SerializeField] TextMeshPro dayUI;
    [SerializeField] TextMeshPro moneyUI;
    [SerializeField] GameObject UIDishCard;
    [SerializeField] Sprite[] timerSprites;
    [SerializeField] SpriteRenderer timer;
    [SerializeField] TextMeshPro timerCountDown;
    [SerializeField] FadingTransition fadingTransition;
    [SerializeField] GameObject[] observerGameObjects;
    List<Observer> observers;
    private float timeLeft;
    private float dayLength = 60; // Set to 300 or 60?
    bool levelRunning;

    void Start() {
        if (controller == null) {
            Debug.LogError("controller script is null");
        }
        observers = new List<Observer>();
        foreach (GameObject gameObject in observerGameObjects) {
            observers.Add(gameObject.GetComponent<Observer>());
        }
        levelRunning = true;
        UpdateMoneyUI();
        UpdateDayUI();
        timeLeft = dayLength;
        timerCountDown.text = timeLeft.ToString();
    }

    void Update() {
        if (levelRunning) {
            timeLeft -= Time.deltaTime;
            timerCountDown.text = Mathf.Max(Mathf.Floor(timeLeft), 0).ToString();
            UpdateTimerSprite();
            if (controller.CheckIfCustomerCountIsZero() && Time.timeScale > 0) {
                levelRunning = false;
                EndLevel();
            }
        }
    }

    public void UpdateMoneyUI() {
        moneyUI.text = PlayerData.money.ToString();
    }

    public void UpdateDayUI() {
        dayUI.text = "Day " + PlayerData.day.ToString();
    }

    public float GetTimeLeft() {
        return timeLeft;
    }

    public void UpdateTimerSprite() {
        float percentage = (timeLeft / dayLength) * 100f;
        int index = 28 - Mathf.Clamp(Mathf.RoundToInt(percentage * (timerSprites.Length - 1) / 100f), 0, timerSprites.Length - 1);
        timer.sprite = timerSprites[index];
    }

    public void EndLevel() {
        GameState.PauseGame();
        UpdateObserver();
        UIDishCard.SetActive(false);
        controller.UpdateSummary();
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
