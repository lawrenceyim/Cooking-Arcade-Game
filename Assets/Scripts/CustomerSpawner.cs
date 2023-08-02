using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    float spawnCooldown = 5f;
    OrderManager orderManager;
    Vector3 spawnPosition = new Vector3(-10f, .4f, 9f);
    Timer timer;
    [SerializeField] int customerCount = 0;
    int customerLimit = 6;
    public delegate float DataDelegate();
    public DataDelegate checkTimeLeft;

    void Start()
    {
        orderManager = GetComponent<OrderManager>();
        timer = gameObject.AddComponent<Timer>();
        SpawnCustomer();
        InvokeNextCustomerSpawn();
        checkTimeLeft = GameObject.Find("DataPanel").GetComponent<DataUI>().GetTimeLeft;
    }

    // Spawns a customer and sets a timer to spawn the next customer recursively
    void InvokeNextCustomerSpawn() {
        timer.SetTimer(spawnCooldown, () => {
            if (customerCount < customerLimit) {
                SpawnCustomer();
            }
            if (checkTimeLeft() > 5) {
                InvokeNextCustomerSpawn();
            } 
        });
    }

    void SpawnCustomer() {
        IncreaseCustomerCount();
        int randomCustomerIndex = UnityEngine.Random.Range(0, PrefabCache.instance.customerPrefab.Length);
        GameObject customer = Instantiate(PrefabCache.instance.customerPrefab[randomCustomerIndex], spawnPosition, Quaternion.identity);
        customer.GetComponent<Customer>().SetDelegate(DecreaseCustomerCount);
    }

    public void IncreaseCustomerCount() {
        customerCount++;
    }

    public void DecreaseCustomerCount() {
        customerCount--;
    }

    public bool NoCustomersLeft() {
        return customerCount == 0;
    }
}
