using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dressing : MonoBehaviour
{
    [SerializeField] Controller controller;
    float[] ranchAmount;
    float[] thousandAmount;
    float[] vinaigretteAmount;
    GameObject currentRanch;
    GameObject currentThousand;
    GameObject currentVinaigrette;
    float lessThreshold = 0.5f;
    float regularThreshold = 1.5f;
    float extraThreshold = 2.5f;
    
    void Start()
    {
        ranchAmount = new float[6];
        thousandAmount = new float[6];
        vinaigretteAmount = new float[6];
    }

    void Update()
    {

    }

    public void RemoveDressing(int index) {
        ranchAmount[index] = 0;
        thousandAmount[index] = 0;
        vinaigretteAmount[index] = 0;
        DestroyCurrentDressing();
    }

    public void DestroyCurrentDressing() {
        DestroyCurrentRanch();
        DestroyCurrentThousand();
        DestroyCurrentVinaigrette();
    }

    public void DestroyCurrentRanch() {
        if (currentRanch != null) {
            Destroy(currentRanch);
            currentRanch = null;
        }
    }

    public void DestroyCurrentThousand() {
        if (currentThousand != null) {
            Destroy(currentThousand);
            currentThousand = null;
        }
    }

    public void DestroyCurrentVinaigrette() {
        if (currentVinaigrette != null) {
            Destroy(currentVinaigrette);
            currentVinaigrette = null;
        }
    }

    public void AddRanch(int index, float amount) {
        ranchAmount[index] += amount;
        SetCurrentDressingObject(index, "Ranch");
    }

    public void AddThousand(int index, float amount) {
        thousandAmount[index] += amount;
        SetCurrentDressingObject(index, "Thousand");
    }

    public void AddVinaigrette(int index, float amount) {
        vinaigretteAmount[index] += amount;
        SetCurrentDressingObject(index, "Vinaigrette");
    }

    public void SetCurrentDressingObject(int index, string type) {
        if (type == "Ranch") {
            if (ranchAmount[index] < lessThreshold) {
                DestroyCurrentRanch();
            } else if (ranchAmount[index] < regularThreshold) {
                currentRanch = Instantiate(PrefabCache.instance.lessRanch, new Vector3(0, .5f, -5f), Quaternion.identity);
            } else if (ranchAmount[index] < extraThreshold) {
                currentRanch = Instantiate(PrefabCache.instance.regularRanch, new Vector3(0, .5f, -5f), Quaternion.identity);
            } else {
                currentRanch = Instantiate(PrefabCache.instance.extraRanch, new Vector3(0, .5f, -5f), Quaternion.identity);
            }
        } else if (type == "Thousand") {
            if (thousandAmount[index] < lessThreshold) {
                DestroyCurrentThousand();
            } else if (thousandAmount[index] < regularThreshold) {
                currentThousand = Instantiate(PrefabCache.instance.lessThousand, new Vector3(0, .5f, -5f), Quaternion.identity);
            } else if (thousandAmount[index] < extraThreshold) {
                currentThousand = Instantiate(PrefabCache.instance.regularThousand, new Vector3(0, .5f, -5f), Quaternion.identity);
            } else {
                currentThousand = Instantiate(PrefabCache.instance.extraThousand, new Vector3(0, .5f, -5f), Quaternion.identity);
            }
        } else if (type == "Vinaigrette") {
            if (vinaigretteAmount[index] < lessThreshold) {
                DestroyCurrentVinaigrette();
            } else if (vinaigretteAmount[index] < regularThreshold) {
                currentVinaigrette = Instantiate(PrefabCache.instance.lessVinaigrette, new Vector3(0, .5f, -5f), Quaternion.identity);
            } else if (vinaigretteAmount[index] < extraThreshold) {
                currentVinaigrette = Instantiate(PrefabCache.instance.regularVinaigrette, new Vector3(0, .5f, -5f), Quaternion.identity);
            } else {
                currentVinaigrette = Instantiate(PrefabCache.instance.extraVinaigrette, new Vector3(0, .5f, -5f), Quaternion.identity);
            }
        }
    }

    public bool SauceMatchesOrder(int ranchStatus, int thousandStatus, int vinaigretteStatus) {
        if (ranchStatus == 0 && currentRanch != null) return false;
        else if (ranchStatus == 1 && currentRanch != PrefabCache.instance.lessRanch) return false;
        else if (ranchStatus == 2 && currentRanch != PrefabCache.instance.regularRanch) return false;
        else if (ranchStatus == 3 && currentRanch != PrefabCache.instance.extraRanch) return false;
        if (thousandStatus == 0 && currentThousand != null) return false;
        else if (thousandStatus == 1 && currentThousand != PrefabCache.instance.lessThousand) return false;
        else if (thousandStatus == 2 && currentThousand != PrefabCache.instance.regularThousand) return false;
        else if (thousandStatus == 3 && currentThousand != PrefabCache.instance.extraThousand) return false;
        if (vinaigretteStatus == 0 && currentVinaigrette != null) return false;
        else if (vinaigretteStatus == 1 && currentVinaigrette != PrefabCache.instance.lessVinaigrette) return false;
        else if (vinaigretteStatus == 2 && currentVinaigrette != PrefabCache.instance.regularVinaigrette) return false;
        else if (vinaigretteStatus == 3 && currentVinaigrette != PrefabCache.instance.extraVinaigrette) return false;
        return true;
    }

    public float GetRanchAmount(int index) {
        return ranchAmount[index];
    }

    public float GetThousandAmount(int index) {
        return thousandAmount[index];
    }

    public float GetVinaigretteAmount(int index) {
        return vinaigretteAmount[index];
    }
}
