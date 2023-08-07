using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataUI : MonoBehaviour
{
    [SerializeField] Controller controller;
    [SerializeField] TextMeshPro dayUI;
    [SerializeField] TextMeshPro moneyUI;
    [SerializeField] TextMeshPro timerUI;
    private float timeLeft;

    void Start()
    {
        if (controller == null) {
            Debug.LogError("controller script is null");
        }
        UpdateMoneyUI();
        UpdateDayUI();
        timeLeft = 300f;
    }

    void Update()
    {
        if (timeLeft > 0) {
            timeLeft -= Time.deltaTime;
            timerUI.text = ((int) timeLeft).ToString();
            if (timeLeft <= 0) {
                timerUI.text = "0";
            }
        } else {
            if (controller.CheckIfCustomerCountIsZero()) {
                EndLevel();
            }
        }
    }

    public void UpdateMoneyUI() {
        moneyUI.text = "$" + PlayerData.money.ToString();
    }

    public void UpdateDayUI() {
        dayUI.text = "Day " + PlayerData.day.ToString();
    }

    public float GetTimeLeft() {
        return timeLeft;
    }

    void EndLevel() {
        PlayerData.IncrementDay();
        PlayerData.SaveData();
        // Open a menu that displays stats for the day and give option to return to main menu, quit game,
        // or start next day. Should indicate to player that the game has been saved as well
    }
}
