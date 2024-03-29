using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverOverSpriteChange : MonoBehaviour {
    [SerializeField] Sprite originalSprite;
    [SerializeField] Sprite hoverOverSprite;
    SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMouseOver() {
        spriteRenderer.sprite = hoverOverSprite;
    }

    public void OnMouseExit() {
        spriteRenderer.sprite = originalSprite;
    }

}
