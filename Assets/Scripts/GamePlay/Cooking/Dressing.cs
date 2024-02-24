using UnityEngine;

public class Dressing : MonoBehaviour {
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
        DestroyCurrentDressing();
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
}
