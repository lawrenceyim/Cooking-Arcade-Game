using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataUI : MonoBehaviour
{
    [SerializeField] TextMeshPro moneyUI;
    [SerializeField] TextMeshPro timerUI;
    private float timeLeft;
    public delegate bool CustomerSpawnerDelegate();
    CustomerSpawnerDelegate noCustomersLeft;


    void Start()
    {
        UpdateMoneyUI();
        timeLeft = 300f;
        noCustomersLeft = GameObject.Find("LevelManager").GetComponent<CustomerSpawner>().NoCustomersLeft;
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
            if (noCustomersLeft()) {
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
