using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverOverImageSpriteChange : MonoBehaviour {
    [SerializeField] Sprite originalSprite;
    [SerializeField] Sprite hoverOverSprite;
    Image sourceImage;

    private void Start() {
        sourceImage = GetComponent<Image>();
    }

    public void OnMouseOver() {
        sourceImage.sprite = hoverOverSprite;
    }

    public void OnMouseExit() {
        sourceImage.sprite = originalSprite;
    }

}
