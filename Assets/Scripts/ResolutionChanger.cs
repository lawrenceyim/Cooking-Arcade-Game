using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ResolutionChanger : MonoBehaviour
{
    [SerializeField] int xResolution;
    [SerializeField] int yResolution;

    public void ChangeScreenResolution() {
        Screen.SetResolution(xResolution, yResolution, false);
    }
}
