using UnityEngine;

public class Oven : MonoBehaviour {
    GameObject currentPizza;

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

    public void SetPizzaGameObject(int status) {
        if (status == 0) {
            if (currentPizza != null) {
                DestroyCurrentPizza();
                return;
            }
        } else if (status == 1) {
            if (currentPizza != PrefabCache.instance.rawPizza) {
                DestroyCurrentPizza();
                InstantiateCurrentPizza(PrefabCache.instance.rawPizza);
            }
        } else if (status == 2) {
            if (currentPizza != PrefabCache.instance.cookedPizza) {
                DestroyCurrentPizza();
                InstantiateCurrentPizza(PrefabCache.instance.cookedPizza);
            }
        } else if (status == 3) {
            if (currentPizza != PrefabCache.instance.burntPizza) {
                DestroyCurrentPizza();
                InstantiateCurrentPizza(PrefabCache.instance.burntPizza);
            }
        }
    }
}
