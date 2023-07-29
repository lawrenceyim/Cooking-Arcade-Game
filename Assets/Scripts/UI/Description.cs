using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Description : MonoBehaviour
{
    [SerializeField] TextMeshPro orderName;
    [SerializeField] TextMeshPro orderDescription;
    [SerializeField] TextMeshPro OrderCountdown;
    float countdown;

    private void Start() {
        orderName.text = "";
        orderDescription.text = "";
    }

    public void SetOrderDescription(string name, float countdown) {
        orderName.text = name;
        orderDescription.text = Recipe.recipeDescription[name];
        this.countdown = countdown;
    }

    void Update() {
        // countdown -= Time.deltaTime;
        // OrderCountdown.text = ((int) countdown).ToString();
    }

    public OrderManager.DescriptionUIDelegate GetDescriptionDelegate() {
        return SetOrderDescription;
    }
}
