using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    int numberOfOrders = 6;
    string[] orders;
    GameObject[] customers;
    Description descriptionScript;
    OrderUI orderUIScript;
    public delegate void CookingUIDelegate(string name, int index);
    CookingUIDelegate updateCookingUI;
    public delegate void CookingUIResetDelegate();
    CookingUIResetDelegate resetCookingUI;
    int currentIndex;
    Cooking cookingScript;
    DataUI dataUI;


    void Start()
    {   
        orders = new string[numberOfOrders];
        customers = new GameObject[numberOfOrders];
        descriptionScript = GameObject.Find("DescriptionPanel").GetComponent<Description>();
        orderUIScript = GameObject.Find("OrderPanel").GetComponent<OrderUI>();
        updateCookingUI = GameObject.Find("CookingPanel").GetComponent<CookingUI>().UpdateButtons;
        resetCookingUI = GameObject.Find("CookingPanel").GetComponent<CookingUI>().DeactivateButtons;
        cookingScript = GameObject.Find("DishPanel").GetComponent<Cooking>();
        dataUI = GameObject.Find("DataPanel").GetComponent<DataUI>();

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
                descriptionScript.SetOrderDescription(orders[i - 1]);
                updateCookingUI(orders[i - 1], currentIndex);
                cookingScript.ChangeDish(currentIndex);
            }
        }
    }

    public bool hasEmptyOrderSlot() {
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
                // orders[i] = Recipe.SelectRandomRecipe();
                orders[i] = "Cheeseburger";
                orderUIScript.SetOrderSlot(i, orders[i], 30f);
                customers[i] = customer;
                cookingScript.AddDish(i, Instantiate(Recipe.dishes[orders[i]], new Vector3(0f, 0f, 0f), Quaternion.identity), orders[i]);
                break;
            }
        }
    }

    public void RemoveOrder(int orderIndex) {
        customers[orderIndex].GetComponent<Customer>().ChangeState();
        orders[orderIndex] = null;
        customers[orderIndex] = null;
        cookingScript.RemoveDish(orderIndex);
        orderUIScript.ResetOrderSlot(orderIndex);
        if (orderIndex == currentIndex) {
            descriptionScript.ClearOrderDescription();
            resetCookingUI();
        }
    }

    public void OrderServed() {
        PlayerData.money += Recipe.sellingPrice[orders[currentIndex]];
        dataUI.UpdateMoneyUI();
        RemoveOrder(currentIndex);
    }

    public bool HasOrder(int index) {
        return orders[index] != null;
    }
}
