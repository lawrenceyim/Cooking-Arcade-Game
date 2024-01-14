using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderManager : MonoBehaviour {
    [SerializeField] Controller controller;
    [SerializeField] TextMeshProUGUI[] orderNumberText;
    Color highlightedColor = Color.yellow;
    Color regularColor = Color.white;

    int numberOfOrders = 6;
    Dish[] orders;
    GameObject[] customers;
    int currentIndex = -1;

    void Start() {
        if (controller == null) {
            Debug.LogError("controller script is null");
        }
        orders = new Dish[numberOfOrders];
        customers = new GameObject[numberOfOrders];
    }

    private void Update() {
        ProcessInput();
    }

    void ProcessInput() {
        // Detect numeric key press
        for (int i = 1; i <= numberOfOrders; i++) {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i) && orders[i - 1] != null && currentIndex != i - 1) {
                currentIndex = i - 1;
                controller.SetOrderDescription(orders[i - 1]);
                controller.UpdateCookingButtons(currentIndex);
                controller.ChangeDishBeingWorkedOn(currentIndex);
                ResetOrderNumberColor();
                HightlightOrderNumber(currentIndex);
            }
        }
    }

    public bool HasEmptyOrderSlot() {
        for (int i = 0; i < numberOfOrders; i++) {
            if (orders[i] == null) {
                return true;
            }
        }
        return false;
    }

    public void AddOrder(GameObject customer) {
        for (int i = 0; i < numberOfOrders; i++) {
            if (orders[i] == null) {
                orders[i] = Recipe.SelectRandomRecipe();
                controller.AddOrderToOrderSlot(i, orders[i].dishName, 30f);
                customers[i] = customer;
                controller.AddDishToCookingPanel(i, Instantiate(PrefabCache.instance.dishDict[orders[i].dishName], new Vector3(0f, -.4f, 0f), Quaternion.identity));
                controller.AddActivity(orders[i], i);
                break;
            }
        }
    }

    public void RemoveOrder(int orderIndex) {
        customers[orderIndex].GetComponent<Customer>().ChangeState();
        orders[orderIndex] = null;
        customers[orderIndex] = null;
        controller.ResetCookingUI(orderIndex);

        controller.RemoveDishFromCookingPanel(orderIndex);
        controller.ResetOrderSlot(orderIndex);

        if (orderIndex == currentIndex) {
            AudioManager.instance.StopPlayingSound();
            controller.ClearDescriptionPanel();
            controller.ClearCookingButtons();
            controller.HideStations();
            controller.HideHud();
            ResetOrderNumberColor();
            currentIndex = -1;
        }
    }

    public void OrderServed() {
        PlayerData.IncreaseMoney(orders[currentIndex].sellingPrice);
        controller.UpdateMoneyUI();
        RemoveOrder(currentIndex);
        ResetOrderNumberColor();
        AudioManager.instance.PlayCoinSound();
    }

    public bool HasOrder(int index) {
        return orders[index] != null;
    }

    public void ResetOrderNumberColor() {
        for (int i = 0; i < numberOfOrders; i++) {
            orderNumberText[i].color = regularColor;
        }
    }

    public void HightlightOrderNumber(int orderIndex) {
        if (orderIndex < 0 || orderIndex >= 6) return;
        orderNumberText[orderIndex].color = highlightedColor;
    }
}
