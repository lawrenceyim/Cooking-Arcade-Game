using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabCache : MonoBehaviour
{
    public static PrefabCache instance;
    [SerializeField] public GameObject CheeseBurger;
    [SerializeField] public GameObject[] customerPrefab;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

}
