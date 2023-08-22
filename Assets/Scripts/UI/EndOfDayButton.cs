using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EndOfDayButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Sprite originalSprite;
    [SerializeField] Sprite hoverOverSprite;
    Image image;

    private void Start() {
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = hoverOverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = originalSprite;  
    }
}
