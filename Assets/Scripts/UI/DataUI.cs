using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataUI : MonoBehaviour
{
    [SerializeField] Controller controller;
    [SerializeField] TextMeshPro dayUI;
    [SerializeField] TextMeshPro moneyUI;
    // [SerializeField] TextMeshPro timerUI;
    [SerializeField] GameObject UIDishCard;
    private float timeLeft;
    private float dayLength = 60 * 5f;
    bool levelRunning;

    void Start()
    {
        if (controller == null) {
            Debug.LogError("controller script is null");
        }
        levelRunning = true;
        UpdateMoneyUI();
        UpdateDayUI();
        timeLeft = dayLength;
    }

    void Update()
    {
        if (timeLeft > 0) {
            // timeLeft -= Time.deltaTime;
            // timerUI.text = ((int) timeLeft).ToString();
            // if (timeLeft <= 0) {
            //     timerUI.text = "0";
            // }
        } else if (levelRunning) {
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

    void EndLevel() {
        Time.timeScale = 0f;
        UIDishCard.SetActive(false);
        controller.UpdateSummary();
    }
}
