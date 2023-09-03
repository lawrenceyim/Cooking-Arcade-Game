using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
[SerializeField] Controller controller;
    float[] cookingTimer;
    GameObject currentPizza;
    int[] pizzaStatus; // 0 no pizza, 1 raw pizza, 2 cooked pizza, 3 burnt pizza
    float cookingTime;

    void Start()
    {
        cookingTime = 5f;
        cookingTimer = new float[6];    
        pizzaStatus = new int[6];
    }

    void Update()
    {
        for (int i = 0; i < 6; i++) {
            if (pizzaStatus[i] == 0) continue;
            cookingTimer[i] -= Time.deltaTime;
            if (cookingTimer[i] <= 0) {
                if (pizzaStatus[i] == 3) continue;
                else if (pizzaStatus[i] == 1) {
                    cookingTimer[i] = cookingTime;
                    pizzaStatus[i] = 2;
                } else if (pizzaStatus[i] == 2) {
                    pizzaStatus[i] = 3;
                }
            }
        }
    }

    public void AddPizzaToOven(int index) {
        pizzaStatus[index] = 1;
        cookingTimer[index] = cookingTime;
    }

    public void RemovePizzaFromOven(int index) {
        pizzaStatus[index] = 0;
        cookingTimer[index] = 0;
    }

    public void InstantiateCurrentPizza(GameObject pizza) {
        if (pizza == PrefabCache.instance.burntPizza) {
            currentPizza = Instantiate(pizza, new Vector3(0, 1, -5), Quaternion.identity);
            return;
        }
        currentPizza = Instantiate(pizza, new Vector3(0, 0, -5), Quaternion.identity);
    }

    public void DestroyCurrentPizza() {
        if (currentPizza == null) return;
        Destroy(currentPizza);
        currentPizza = null;
    }

    public int GetPizzaStatus(int index) {
        return pizzaStatus[index];
    }
    
    public float GetPizzaTimer(int index) {
        return cookingTimer[index];
    }

    public void SetPizzaStatus(int index, int status) {
        pizzaStatus[index] = status;
    }

    public void SetPizzaGameObject(int index) {
        if (pizzaStatus[index] == 0) {
            if (currentPizza != null) {
                DestroyCurrentPizza();
                return;
            }
        } else if (pizzaStatus[index] == 1) {
            if (currentPizza != PrefabCache.instance.rawPizza) {
                DestroyCurrentPizza();
                InstantiateCurrentPizza(PrefabCache.instance.rawPizza);
            }
        } else if (pizzaStatus[index] == 2) {
            if (currentPizza != PrefabCache.instance.cookedPizza) {
                DestroyCurrentPizza();
                InstantiateCurrentPizza(PrefabCache.instance.cookedPizza);
            }
        } else if (pizzaStatus[index] == 3) {
            if (currentPizza != PrefabCache.instance.burntPizza) {
                DestroyCurrentPizza();
                InstantiateCurrentPizza(PrefabCache.instance.burntPizza);
            }
        }
    }
}
