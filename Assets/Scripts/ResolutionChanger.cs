using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ResolutionChanger : MonoBehaviour {
    [SerializeField] int xResolution;
    [SerializeField] int yResolution;

    public void ChangeScreenResolution() {
        ES3.Save("xResolution", xResolution);
        ES3.Save("yResolution", yResolution);
        Screen.SetResolution(xResolution, yResolution, false);
    }
}
