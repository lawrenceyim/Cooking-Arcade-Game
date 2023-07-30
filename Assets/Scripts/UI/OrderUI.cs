using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderUI : MonoBehaviour
{
    [SerializeField] GameObject[] orderBlocks;
    float[] countdowns;
    TextMeshPro[] texts;
    TextMeshPro[] countdownTexts;
    OrderManager orderManager;

    void Start()
    {
        countdowns = new float[orderBlocks.Length];
        texts = new TextMeshPro[orderBlocks.Length];
        countdownTexts = new TextMeshPro[orderBlocks.Length];
        for (int i = 0; i < orderBlocks.Length; i++) {
            texts[i] = orderBlocks[i].transform.Find("Text").GetComponent<TextMeshPro>();
            countdownTexts[i] = orderBlocks[i].transform.Find("Countdown").GetComponent<TextMeshPro>();
            texts[i].text = "";
            countdownTexts[i].text = "";
        }
        orderManager = GameObject.Find("LevelManager").GetComponent<OrderManager>();
    }

    void Update()
    {
        for (int i = 0; i < orderBlocks.Length; i++) {
            if (orderManager.HasOrder(i)) {
                countdowns[i] -= Time.deltaTime;
                countdownTexts[i].text = ((int)countdowns[i]).ToString();
                if (countdowns[i] <= 0) {
                    ResetOrderSlot(i);
                    orderManager.RemoveOrder(i);
                }
            } 
        }
    }

    public void SetOrderSlot(int index, string name, float countdown) {
        texts[index].text = name;
        countdowns[index] = countdown;
        countdownTexts[index].text = ((int) countdown).ToString();
    }

    public void ResetOrderSlot(int index) {
        texts[index].text = "";
        countdownTexts[index].text = "";
    }
}
