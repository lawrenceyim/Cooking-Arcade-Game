using UnityEngine;
using UnityEngine.UI;

public class DisableLoadGameButton : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] BoxCollider2D button;
    Color availableColor = new Color(255, 255, 255, 1f);
    Color disabledColor = new Color(0, 0, 0, .5f);

    void Start()
    {
        if (!PlayerData.saveFileExists) {
            spriteRenderer.color = disabledColor;
            button.enabled = false;
        } else {
            spriteRenderer.color = availableColor;
            button.enabled = true;
        }
    }
}
