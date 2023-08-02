using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] Controller controller;
    int numberOfOrders = 6;
    Dish[] orders;
    GameObject[] customers;
    int currentIndex;

    void Start()
    {   
        orders = new Dish[numberOfOrders];
        customers = new GameObject[numberOfOrders];
    }

    private void Update() {
        ProcessInput();
    }

    void ProcessInput() {
        // Detect numeric key press
        for (int i = 1; i <= numberOfOrders; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i) && orders[i - 1] != null)
            {
                currentIndex = i - 1;
                controller.SetOrderDescription(orders[i - 1]);
                controller.UpdateCookingButtons(orders[i - 1], currentIndex);
                controller.ChangeDishBeingWorkedOn(currentIndex);
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
                controller.AddDishToCookingPanel(i, Instantiate(PrefabCache.instance.dishDict[orders[i].dishName], new Vector3(0f, 0f, 0f), Quaternion.identity));
                break;
            }
        }
    }

    public void RemoveOrder(int orderIndex) {
        customers[orderIndex].GetComponent<Customer>().ChangeState();
        orders[orderIndex] = null;
        customers[orderIndex] = null;
        controller.RemoveDishFromCookingPanel(orderIndex);
        controller.ResetOrderSlot(orderIndex);
        if (orderIndex == currentIndex) {
            controller.ClearDescriptionPanel();
            controller.ClearCookingButtons();
        }
    }

    public void OrderServed() {
        AudioManager.instance.PlayCoinSound();
        PlayerData.money += orders[currentIndex].sellingPrice;
        controller.UpdateMoneyUI();
        RemoveOrder(currentIndex);
    }

    public bool HasOrder(int index) {
        return orders[index] != null;
    }
}
