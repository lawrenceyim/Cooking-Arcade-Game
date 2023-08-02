using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderUI : MonoBehaviour
{
    [SerializeField] Controller controller;
    [SerializeField] GameObject[] orderBlocks;
    float[] countdowns; 
    SpriteRenderer[] spriteRenderers;
    TextMeshPro[] countdownTexts;

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
    }

    void Update()
    {
        for (int i = 0; i < orderBlocks.Length; i++) {
            if (controller.OrderSlotIsOccupied(i)) {
                countdowns[i] -= Time.deltaTime;
                countdownTexts[i].text = ((int)countdowns[i]).ToString();
                if (countdowns[i] <= 0) {
                    ResetOrderSlot(i);
                    controller.RemoveOrderFromOrderSlot(i);
                }
            } 
        }
    }

    public void SetOrderSlot(int index, Recipe.DishName dishName, float countdown) {
        spriteRenderers[index].sprite = PrefabCache.instance.dishIconDict[dishName];
        spriteRenderers[index].enabled = true;
        countdowns[index] = countdown;
        countdownTexts[index].text = ((int) countdown).ToString();
    }

    public void ResetOrderSlot(int index) {
        spriteRenderers[index].enabled = false;
        countdownTexts[index].text = "";
    }

}
