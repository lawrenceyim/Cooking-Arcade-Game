using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dressing : MonoBehaviour
{
    [SerializeField] Controller controller;
    float[] ranchAmount;
    float[] thousandAmount;
    float[] vinagriatteAmount;
    GameObject currentRanch;
    GameObject currentThousand;
    GameObject currentVinagriatte;
    float lessThreshold = 1.5f;
    float regularThreshold = 3.0f;
    float extraThreshold = 4.5f;
    
    void Start()
    {
        ranchAmount = new float[6];
        thousandAmount = new float[6];
        vinagriatteAmount = new float[6];
    }

    void Update()
    {

    }

    public void RemoveDressing(int index) {
        ranchAmount[index] = 0;
        thousandAmount[index] = 0;
        vinagriatteAmount[index] = 0;
        DestroyCurrentDressing();
    }

    public void DestroyCurrentDressing() {
        DestroyCurrentRanch();
        DestroyCurrentThousand();
        DestroyCurrentVinagriatte();
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

    public void DestroyCurrentVinagriatte() {
        if (currentVinagriatte != null) {
            Destroy(currentVinagriatte);
            currentVinagriatte = null;
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

    public void AddVinagriatte(int index, float amount) {
        vinagriatteAmount[index] += amount;
        SetCurrentDressingObject(index, "Vinagriatte");
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
        } else if (type == "Vinagriatte") {
            if (vinagriatteAmount[index] < lessThreshold) {
                DestroyCurrentVinagriatte();
            } else if (vinagriatteAmount[index] < regularThreshold) {
                currentVinagriatte = Instantiate(PrefabCache.instance.lessVinagriatte, new Vector3(0, .5f, -5f), Quaternion.identity);
            } else if (vinagriatteAmount[index] < extraThreshold) {
                currentVinagriatte = Instantiate(PrefabCache.instance.regularVinagriatte, new Vector3(0, .5f, -5f), Quaternion.identity);
            } else {
                currentVinagriatte = Instantiate(PrefabCache.instance.extraVinagriatte, new Vector3(0, .5f, -5f), Quaternion.identity);
            }
        }
    }

    public bool SauceMatchesOrder(int ranchStatus, int thousandStatus, int vinagriatteStatus) {
        if (ranchStatus == 0 && currentRanch != null) return false;
        else if (ranchStatus == 1 && currentRanch != PrefabCache.instance.lessRanch) return false;
        else if (ranchStatus == 2 && currentRanch != PrefabCache.instance.regularRanch) return false;
        else if (ranchStatus == 3 && currentRanch != PrefabCache.instance.extraRanch) return false;
        if (thousandStatus == 0 && currentThousand != null) return false;
        else if (thousandStatus == 1 && currentThousand != PrefabCache.instance.lessThousand) return false;
        else if (thousandStatus == 2 && currentThousand != PrefabCache.instance.regularThousand) return false;
        else if (thousandStatus == 3 && currentThousand != PrefabCache.instance.extraThousand) return false;
        if (vinagriatteStatus == 0 && currentVinagriatte != null) return false;
        else if (vinagriatteStatus == 1 && currentVinagriatte != PrefabCache.instance.lessVinagriatte) return false;
        else if (vinagriatteStatus == 2 && currentVinagriatte != PrefabCache.instance.regularVinagriatte) return false;
        else if (vinagriatteStatus == 3 && currentVinagriatte != PrefabCache.instance.extraVinagriatte) return false;
        return true;
    }
}
