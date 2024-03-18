using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour {
    [SerializeField] Controller controller;
    float spawnCooldown = 3f;
    Vector3 spawnPosition = new Vector3(-10f, -.55f, 25f);
    Timer timer;
    int customerCount = 0;
    int customerLimit = 6;
    Queue<float> travelTimes;

    void Start() {
        if (controller == null) {
            Debug.LogError("controller script is null");
        }
        Recipe.SetDistList(PlayerData.day);
        timer = gameObject.AddComponent<Timer>();
        SetTravelTimes();
        SpawnCustomer();
        InvokeNextCustomerSpawn();
    }

    private void SetTravelTimes() {
        travelTimes = new Queue<float>();
        travelTimes.Enqueue(1.5f);
        travelTimes.Enqueue(2.2f);
        travelTimes.Enqueue(2.9f);
        travelTimes.Enqueue(3.4f);
        travelTimes.Enqueue(4.1f);
        travelTimes.Enqueue(4.8f);
    }

    private float GetTravelTime() {
        return travelTimes.Dequeue();
    }

    public void ReturnTravelTime(float travelTime) {
        travelTimes.Enqueue(travelTime);
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
        customer.GetComponent<Customer>().Initialize(this, GetTravelTime());
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
