using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    int numberOfOrders = 6;
    string[] orders;
    [SerializeField] string[] recipeNames;  // Can be set in the scene to determine which recipes will appear in each level 
    public delegate void DescriptionUIDelegate(string name, string desc, float countdown);
    DescriptionUIDelegate updateDescriptionUI;
    public delegate void OrderUIDelegate(int index, string name, float countdown);
    OrderUIDelegate updateOrderUI;

    void Start()
    {   
        orders = new string[numberOfOrders];
        updateDescriptionUI = GameObject.Find("DescriptionPanel").GetComponent<Description>().GetOrderDelegate();
        updateOrderUI = GameObject.Find("OrderPanel").GetComponent<OrderUI>().GetOrderDelegate();
        Recipe.InitializeRecipes();
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
                updateDescriptionUI(orders[i - 1], Recipe.recipeDescription[orders[i - 1]], 59f);
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

    public void AddOrder() {
        for (int i = 0; i < numberOfOrders; i++) {
            if (orders[i] == null) {
                int recipeIndex = UnityEngine.Random.Range(0, recipeNames.Length);
                orders[i] = recipeNames[recipeIndex];
                updateOrderUI(i, orders[i], 59f);
                break;
            }
        }
    }

    public void RemoveOrder(int orderIndex) {
        orders[orderIndex] = null;
    }

    public bool HasOrder(int index) {
        return orders[index] != null;
    }
}
