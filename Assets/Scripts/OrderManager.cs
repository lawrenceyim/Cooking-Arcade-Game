using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    int numberOfOrders = 6;
    string[] orders;
    GameObject[] customers;
    [SerializeField] string[] recipeNames;  // Can be set in the scene to determine which recipes will appear in each level 
    public delegate void DescriptionUIDelegate(string name, float countdown);
    DescriptionUIDelegate updateDescriptionUI;
    public delegate void OrderUIDelegate(int index, string name, float countdown);
    OrderUIDelegate updateOrderUI;
    public delegate void CookingUIDelegate(string name);
    CookingUIDelegate updateCookingUI;
    public delegate void CookingUIResetDelegate();
    CookingUIResetDelegate resetCookingUI;
    int currentIndex;

    void Start()
    {   
        orders = new string[numberOfOrders];
        customers = new GameObject[numberOfOrders];
        updateDescriptionUI = GameObject.Find("DescriptionPanel").GetComponent<Description>().GetDescriptionDelegate();
        updateOrderUI = GameObject.Find("OrderPanel").GetComponent<OrderUI>().GetOrderDelegate();
        updateCookingUI = GameObject.Find("CookingPanel").GetComponent<CookingUI>().GetCookingDelegate();
        resetCookingUI = GameObject.Find("CookingPanel").GetComponent<CookingUI>().GetCookingResetDelegate();
        Recipe.InitializeRecipes();
        Recipe.InitializeKeys();
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
                updateDescriptionUI(orders[i - 1], 59f);
                updateCookingUI(orders[i - 1]);
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
                int recipeIndex = UnityEngine.Random.Range(0, recipeNames.Length);
                orders[i] = recipeNames[recipeIndex];
                updateOrderUI(i, orders[i], 30f);
                customers[i] = customer;
                break;
            }
        }
    }

    public void RemoveOrder(int orderIndex) {
        customers[orderIndex].GetComponent<Customer>().ChangeState();
        orders[orderIndex] = null;
        customers[orderIndex] = null;
        if (orderIndex == currentIndex) {
            resetCookingUI();
        }
    }

    public bool HasOrder(int index) {
        return orders[index] != null;
    }
}
