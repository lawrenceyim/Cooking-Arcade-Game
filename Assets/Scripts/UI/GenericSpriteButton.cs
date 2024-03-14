using UnityEngine;

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
