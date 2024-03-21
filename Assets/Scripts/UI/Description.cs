using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Description : MonoBehaviour {
    [SerializeField] Controller controller;
    [SerializeField] TextMeshProUGUI orderName;
    [SerializeField] TextMeshProUGUI orderDescription;

    private void Start() {
        if (controller == null) {
            Debug.LogError("controller script is null");
        }
        orderName.text = "";
        orderDescription.text = "";
    }

    public void SetOrderDescription(Dish dish) {
        orderName.text = Recipe.dishNameString[dish.dishName];
        orderDescription.text = dish.description;
    }

    public void SetOrderNameOnly(Dish dish) {
        orderName.text = Recipe.dishNameString[dish.dishName];
        orderDescription.text = "";
    }

    public void SetDescription(string desc) {
        orderName.text = "";
        orderDescription.text = desc;
    }

    public void ClearOrderDescription() {
        orderName.text = "";
        orderDescription.text = "";
    }

    void Update() {

    }
}
