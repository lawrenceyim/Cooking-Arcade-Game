using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : MonoBehaviour
{
    [SerializeField] Controller controller;
    float[] cookingTimer;
    GameObject currentPatty;
    int[] pattyStatus; // 0 no patty, 1 raw patty, 2 cooked patty, 3 burnt patty
    float cookingTime;

    void Start()
    {
        cookingTime = 5f;
        cookingTimer = new float[6];    
        pattyStatus = new int[6];
    }

    void Update()
    {
        for (int i = 0; i < 6; i++) {
            if (pattyStatus[i] == 0) continue;
            cookingTimer[i] -= Time.deltaTime;
            if (cookingTimer[i] <= 0) {
                if (pattyStatus[i] == 3) continue;
                else if (pattyStatus[i] == 1) {
                    cookingTimer[i] = cookingTime;
                    pattyStatus[i] = 2;
                } else if (pattyStatus[i] == 2) {
                    pattyStatus[i] = 3;
                }
            }
        }
    }

    public void AddPattyToGrill(int index) {
        pattyStatus[index] = 1;
        cookingTimer[index] = cookingTime;
    }

    public void RemovePattyFromGrill(int index) {
        pattyStatus[index] = 0;
        cookingTimer[index] = 0;
    }

    public void InstantiateCurrentPatty(GameObject patty) {
        currentPatty = Instantiate(patty, new Vector3(0,1,-5), Quaternion.identity);
    }

    public void DestroyCurrentPatty() {
        if (currentPatty == null) return;
        Destroy(currentPatty);
        currentPatty = null;
    }

    public int GetPattyStatus(int index) {
        return pattyStatus[index];
    }
    
    public float GetPattyTimer(int index) {
        return cookingTimer[index];
    }

    public void SetPattyStatus(int index, int status) {
        pattyStatus[index] = status;
    }

    public void SetPattyGameObject(int index) {
        if (pattyStatus[index] == 0) {
            if (currentPatty != null) {
                DestroyCurrentPatty();
                return;
            }
        } else if (pattyStatus[index] == 1) {
            if (currentPatty != PrefabCache.instance.rawPatty) {
                DestroyCurrentPatty();
                InstantiateCurrentPatty(PrefabCache.instance.rawPatty);
            }
        } else if (pattyStatus[index] == 2) {
            if (currentPatty != PrefabCache.instance.cookedPatty) {
                DestroyCurrentPatty();
                InstantiateCurrentPatty(PrefabCache.instance.cookedPatty);
            }
        } else if (pattyStatus[index] == 3) {
            if (currentPatty != PrefabCache.instance.burntPatty) {
                DestroyCurrentPatty();
                InstantiateCurrentPatty(PrefabCache.instance.burntPatty);
            }
        }
    }
}
