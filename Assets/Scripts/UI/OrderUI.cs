using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OrderUI : MonoBehaviour
{
    [SerializeField] Controller controller;
    [SerializeField] GameObject[] orderBlocks;
    float[] timeLeft;
    float[] startingTime; 
    [SerializeField] Slider[] sliders;
    [SerializeField] Image[] sliderFillColorImages;
    [SerializeField] Image[] sliderHandleImages;

    Color greenFill = new Color(0, 200, 0, 1);
    Color yellowFill = new Color(200, 200, 0, 1);
    Color redFill = new Color(200, 0, 0, 1);

    void Start()
    {
        if (controller == null) {
            Debug.LogError("controller script is null");
        }
        timeLeft = new float[orderBlocks.Length];
        startingTime = new float[orderBlocks.Length];
        for (int i = 0; i < orderBlocks.Length; i++) {
            ResetOrderSlot(i);
        }
    }

    void Update()
    {
        for (int i = 0; i < orderBlocks.Length; i++) {
            if (controller.OrderSlotIsOccupied(i)) {
                UpdateSlider(i);
            } 
        }
    }

    public void SetOrderSlot(int index, Recipe.DishName dishName, float time) {
        timeLeft[index] = time;
        startingTime[index] = time;
        sliders[index].value = 1f;
        sliderHandleImages[index].sprite = PrefabCache.instance.dishIconDict[dishName];
        sliderFillColorImages[index].color = greenFill; 
        sliderFillColorImages[index].enabled = true;
        sliderHandleImages[index].enabled = true;
    }

    public void ResetOrderSlot(int index) {
        sliderFillColorImages[index].enabled = false;
        sliderHandleImages[index].enabled = false;
    }

    public void UpdateSlider(int index) {
        timeLeft[index] -= Time.deltaTime;
        float ratio = timeLeft[index] / startingTime[index];
        sliders[index].value = ratio;
        if (ratio <= .33f) {
            sliderFillColorImages[index].color = redFill;
        } else if (ratio <= .66f) {
            sliderFillColorImages[index].color = yellowFill;
        }
        if (timeLeft[index] <= 0) {
            ResetOrderSlot(index);
            controller.RemoveOrderFromOrderSlot(index);
            return;
        }
    }

}
