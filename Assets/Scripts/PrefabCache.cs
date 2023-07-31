using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabCache : MonoBehaviour
{
    public static PrefabCache instance;
    [SerializeField] public List<GameObject> dishes;
    [SerializeField] public GameObject[] customerPrefab;
    public Dictionary<string, GameObject> dishDict;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        dishDict = new Dictionary<string, GameObject>();
        foreach (GameObject dish in dishes) {
            dishDict.Add(dish.GetComponent<Dish>().dishName, dish);
        }

    }

}
