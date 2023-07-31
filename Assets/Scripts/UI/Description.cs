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

    private void Start() {
        orderName.text = "";
        orderDescription.text = "";
    }

    public void SetOrderDescription(Dish dish) {
        orderName.text = dish.dishName;
        orderDescription.text = dish.description;
    }

    public void ClearOrderDescription() {
        orderName.text = "";
        orderDescription.text = "";
    }

    void Update() {
        // countdown -= Time.deltaTime;
        // OrderCountdown.text = ((int) countdown).ToString();
    }
}
