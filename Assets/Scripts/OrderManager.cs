using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    int numberOfOrders = 6;
    Order[] orders;
    [SerializeField] Recipe[] recipes;  // Can be set in the scene to determine which recipes will appear in each level 

    // Start is called before the first frame update
    void Start()
    {   


        orders = new Order[numberOfOrders];
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public bool hasEmptyOrderSlot() {
        for (int i = 0; i < numberOfOrders; i++) {
            if (orders[i] == null) {
                return true;
            }
        }
        return false;
    }

    public void AddOrder(Order newOrder) {
        for (int i = 0; i < numberOfOrders; i++) {
            if (orders[i] == null) {
                orders[i] = newOrder;
            }
        }
    }

    public void RemoveOrder(int orderIndex) {
        orders[orderIndex] = null;
    }
}
