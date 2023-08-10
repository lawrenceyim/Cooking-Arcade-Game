using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] Controller controller;
    float spawnCooldown = 3f;
    Vector3 spawnPosition = new Vector3(-10f, -.5f, 9f);
    Timer timer;
    int customerCount = 0;
    int customerLimit = 6;

    void Start()
    {
        if (controller == null) {
            Debug.LogError("controller script is null");
        }
        timer = gameObject.AddComponent<Timer>();
        SpawnCustomer();
        InvokeNextCustomerSpawn();
    }

    // Spawns a customer and sets a timer to spawn the next customer recursively
    void InvokeNextCustomerSpawn() {
        timer.SetTimer(spawnCooldown, () => {
            if (customerCount < customerLimit) {
                SpawnCustomer();
            }
            if (controller.GetTimeLeft() > 5) {
                InvokeNextCustomerSpawn();
            } 
        });
    }

    void SpawnCustomer() {
        IncreaseCustomerCount();
        int randomCustomerIndex = UnityEngine.Random.Range(0, PrefabCache.instance.customerPrefab.Length);
        GameObject customer = Instantiate(PrefabCache.instance.customerPrefab[randomCustomerIndex], spawnPosition, Quaternion.identity);
        customer.GetComponent<Customer>().SetDelegate(DecreaseCustomerCount);
        customer.GetComponent<Customer>().controller = controller;
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
