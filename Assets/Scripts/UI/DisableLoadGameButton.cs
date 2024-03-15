using UnityEngine;
using UnityEngine.UI;

public class DisableLoadGameButton : MonoBehaviour
{
    [SerializeField]Image image;
    [SerializeField] Button button;
    Color availableColor = new Color(255, 255, 255, 1f);
    Color disabledColor = new Color(0, 0, 0, .5f);

    void Start()
    {
        if (!PlayerData.saveFileExists) {
            image.color = disabledColor;
            button.enabled = false;
        } else {
            image.color = availableColor;
            button.enabled = true;
        }
    }
}
