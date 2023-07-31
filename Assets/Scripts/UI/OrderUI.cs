using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderUI : MonoBehaviour
{
    [SerializeField] GameObject[] orderBlocks;
    float[] countdowns; 
    SpriteRenderer[] spriteRenderers;
    TextMeshPro[] countdownTexts;
    OrderManager orderManager;

    void Start()
    {
        countdowns = new float[orderBlocks.Length];
        spriteRenderers = new SpriteRenderer[orderBlocks.Length];
        countdownTexts = new TextMeshPro[orderBlocks.Length];
        for (int i = 0; i < orderBlocks.Length; i++) {
            spriteRenderers[i] = orderBlocks[i].transform.Find("Icon").gameObject.GetComponent<SpriteRenderer>();
            spriteRenderers[i].enabled = false;
            countdownTexts[i] = orderBlocks[i].transform.Find("Timer").gameObject.transform.Find("Countdown").GetComponent<TextMeshPro>();
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
        spriteRenderers[index].sprite = PrefabCache.instance.dishIconDict[name];
        spriteRenderers[index].enabled = true;
        countdowns[index] = countdown;
        countdownTexts[index].text = ((int) countdown).ToString();
    }

    public void ResetOrderSlot(int index) {
        spriteRenderers[index].enabled = false;
        countdownTexts[index].text = "";
    }

}
