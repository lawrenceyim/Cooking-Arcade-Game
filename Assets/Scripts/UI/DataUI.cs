using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataUI : MonoBehaviour {
    [SerializeField] Controller controller;
    [SerializeField] TextMeshPro dayUI;
    [SerializeField] TextMeshPro moneyUI;
    [SerializeField] GameObject UIDishCard;
    [SerializeField] Sprite[] timerSprites;
    [SerializeField] SpriteRenderer timer;
    [SerializeField] TextMeshPro timerCountDown;
    [SerializeField] FadingTransition fadingTransition;
    private float timeLeft;
    private float dayLength = 60; // Set to 300 or 60?
    bool levelRunning;

    void Start() {
        if (controller == null) {
            Debug.LogError("controller script is null");
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
                fadingTransition.StartFadeIn();
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
        Debug.Log("Ending level called");
        Time.timeScale = 0f;
        UIDishCard.SetActive(false);
        controller.UpdateSummary();
    }
}
