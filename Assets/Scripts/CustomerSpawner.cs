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
    
    void Start()
    {
        orderManager = GetComponent<OrderManager>();
        timer = gameObject.AddComponent<Timer>();
        SpawnCustomer();
        InvokeNextCustomerSpawn();
    }

    void Update()
    {

    }

    // Spawns a customer and sets a timer to spawn the next customer recursively
    void InvokeNextCustomerSpawn() {
        timer.SetTimer(spawnCooldown, () => {
            SpawnCustomer();
            InvokeNextCustomerSpawn();
        });
    }

    void SpawnCustomer() {
        int randomCustomerIndex = Random.Range(0, customerPrefab.Length);
        Instantiate(customerPrefab[randomCustomerIndex], spawnPosition, Quaternion.identity);
    }
}
