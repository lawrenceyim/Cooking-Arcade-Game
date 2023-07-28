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

    public void SetOrderDescription(string name, string description, float countdown) {
        orderName.text = name;
        orderDescription.text = description;
        this.countdown = countdown;
    }

    void Update() {
        // countdown -= Time.deltaTime;
        // OrderCountdown.text = ((int) countdown).ToString();
    }

    public OrderManager.DescriptionUIDelegate GetOrderDelegate() {
        return SetOrderDescription;
    }
}
