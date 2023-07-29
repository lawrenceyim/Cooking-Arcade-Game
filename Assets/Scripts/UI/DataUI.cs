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


    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 30f;
        noCustomersLeft = GameObject.Find("LevelManager").GetComponent<CustomerSpawner>().NoCustomersLeft;
    }

    // Update is called once per frame
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

    public void SetMoneyUI(int money) {
        moneyUI.text = money.ToString();
    }

    public float GetTimeLeft() {
        return timeLeft;
    }

    void EndLevel() {

    }
}
