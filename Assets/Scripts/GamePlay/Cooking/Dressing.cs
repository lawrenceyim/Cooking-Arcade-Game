using UnityEngine;

public class Dressing : MonoBehaviour
{
    GameObject currentRanch;
    GameObject currentThousand;
    GameObject currentVinaigrette;

    public void DestroyCurrentDressing() {
        DestroyCurrentRanch();
        DestroyCurrentThousand();
        DestroyCurrentVinaigrette();
    }

    public void DestroyCurrentRanch() {
        if (currentRanch == null) return;
        Destroy(currentRanch);
        currentRanch = null;
    }

    public void DestroyCurrentThousand() {
        if (currentThousand == null) return;
        Destroy(currentThousand);
        currentThousand = null;
    }

    public void DestroyCurrentVinaigrette() {
        if (currentVinaigrette == null) return;
        Destroy(currentVinaigrette);
        currentVinaigrette = null;
    }

    public void SetCurrentDressingObject(int stage) {
        if (stage <= 2) {
            DestroyCurrentRanch();
        } else if (stage <= 5) {
            DestroyCurrentThousand();
        } else {
            DestroyCurrentVinaigrette();
        }
        
        switch (stage) {
            case 0:
                currentRanch = Instantiate(PrefabCache.instance.lessRanch, new Vector3(0, .5f, -5f), Quaternion.identity);
                break;
            case 1:
                currentRanch = Instantiate(PrefabCache.instance.regularRanch, new Vector3(0, .5f, -5f), Quaternion.identity);
                break;
            case 2:
                currentRanch = Instantiate(PrefabCache.instance.extraRanch, new Vector3(0, .5f, -5f), Quaternion.identity);
                break;
            case 3:
                currentThousand = Instantiate(PrefabCache.instance.lessThousand, new Vector3(0, .5f, -5f), Quaternion.identity);
                break;
            case 4:
                currentThousand = Instantiate(PrefabCache.instance.regularThousand, new Vector3(0, .5f, -5f), Quaternion.identity);
                break;
            case 5:
                currentThousand = Instantiate(PrefabCache.instance.extraThousand, new Vector3(0, .5f, -5f), Quaternion.identity);
                break;
            case 6:
                currentVinaigrette = Instantiate(PrefabCache.instance.lessVinaigrette, new Vector3(0, .5f, -5f), Quaternion.identity);
                break;
            case 7:
                currentVinaigrette = Instantiate(PrefabCache.instance.regularVinaigrette, new Vector3(0, .5f, -5f), Quaternion.identity);
                break;
            case 8:
                currentVinaigrette = Instantiate(PrefabCache.instance.extraVinaigrette, new Vector3(0, .5f, -5f), Quaternion.identity);
                break;
            default:
                break;
        };
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
}
