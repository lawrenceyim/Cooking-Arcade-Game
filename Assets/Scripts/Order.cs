using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    int orderIndex;  // Used to keep track of the Order in the OrderManager's array
    string recipeName;  // Used for the state design pattern

    // Start is called before the first frame update
    void Start()
    {
        switch (recipeName) {
            case "burger":
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
