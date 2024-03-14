using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericSpriteButton : MonoBehaviour
{
    [SerializeField] MonoBehaviour script;
    [SerializeField] string methodName;

    private void OnMouseDown() {
        System.Type type = script.GetType();
        System.Reflection.MethodInfo method = type.GetMethod(methodName);

        method.Invoke(script, null);
    }
}
