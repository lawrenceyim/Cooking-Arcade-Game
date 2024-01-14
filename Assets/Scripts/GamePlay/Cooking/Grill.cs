using UnityEngine;

public class Grill : MonoBehaviour {
    GameObject currentPatty;

    public void InstantiateCurrentPatty(GameObject patty) {
        if (patty == PrefabCache.instance.burntPatty) {
            currentPatty = Instantiate(patty, new Vector3(0, 1.5f, -5), Quaternion.identity);
            return;
        }
        currentPatty = Instantiate(patty, new Vector3(0, 1, -5), Quaternion.identity);
    }

    public void DestroyCurrentPatty() {
        if (currentPatty == null) return;
        Destroy(currentPatty);
        currentPatty = null;
    }

    public void SetPattyGameObject(int status) {
        if (status == 0) {
            if (currentPatty != null) {
                DestroyCurrentPatty();
                return;
            }
        } else if (status == 1) {
            if (currentPatty != PrefabCache.instance.rawPatty) {
                DestroyCurrentPatty();
                InstantiateCurrentPatty(PrefabCache.instance.rawPatty);
            }
        } else if (status == 2) {
            if (currentPatty != PrefabCache.instance.cookedPatty) {
                DestroyCurrentPatty();
                InstantiateCurrentPatty(PrefabCache.instance.cookedPatty);
            }
        } else if (status == 3) {
            if (currentPatty != PrefabCache.instance.burntPatty) {
                DestroyCurrentPatty();
                InstantiateCurrentPatty(PrefabCache.instance.burntPatty);
            }
        }
    }


}
