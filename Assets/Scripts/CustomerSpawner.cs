using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    float spawnCooldown = 5f;
    OrderManager orderManager;
    [SerializeField] GameObject[] customerPrefab;
    Vector3 spawnPosition = new Vector3(-13f, -3f, 9f);
    Timer timer;
    [SerializeField] int customerCount = 0;
    int customerLimit = 6;
    
    void Start()
    {
        Debug.Log(KeyCode.A);
        orderManager = GetComponent<OrderManager>();
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
            InvokeNextCustomerSpawn();
        });
    }

    void SpawnCustomer() {
        IncreaseCustomerCount();
        int randomCustomerIndex = Random.Range(0, customerPrefab.Length);
        GameObject customer = Instantiate(customerPrefab[randomCustomerIndex], spawnPosition, Quaternion.identity);
        customer.GetComponent<Customer>().SetDelegate(DecreaseCustomerCount);
    }

    public void IncreaseCustomerCount() {
        customerCount++;
    }

    public void DecreaseCustomerCount() {
        customerCount--;
    }
}
