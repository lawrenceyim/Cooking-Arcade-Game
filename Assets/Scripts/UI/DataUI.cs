using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataUI : MonoBehaviour
{
    [SerializeField] Controller controller;

    [SerializeField] TextMeshPro moneyUI;
    [SerializeField] TextMeshPro timerUI;
    private float timeLeft;

    void Start()
    {
        UpdateMoneyUI();
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

    public float GetTimeLeft() {
        return timeLeft;
    }

    void EndLevel() {

    }
}
