using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    bool isEmpty;
    
    void Start()
    {
        isEmpty = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsEmpty() {
        return isEmpty;
    }

    public void OccupySeat() {
        isEmpty = false;
    }

    public void LeaveSeat() {
        isEmpty = true;
    }
}
