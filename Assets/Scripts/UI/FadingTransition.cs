using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;

public class FadingTransition : MonoBehaviour {
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject openSign;
    [SerializeField] DataUI dataUI;

    float opacity = 1f;
    float fadingSpeed = .5f;
    Color color = new Color(0f, 0f, 0f);
    bool fadingIn = false;
    bool fadingOut = false;

    void Start() {
        openSign.SetActive(true);
        color.a = 1f;
        spriteRenderer.color = color;
        StartFadeOut();
    }

    void Update() {
        if (GameState.IsGameIsPaused()) {
            return;
        }
        //if (fadingIn) {
        //    opacity += Time.fixedDeltaTime * fadingSpeed;

        //    if (opacity >= 1) {
        //        Debug.Log("Ending level");
        //        fadingIn = false;
        //        opacity = 1;
        //        dataUI.EndLevel();
        //    }

        //    color.a = opacity;
        //    spriteRenderer.color = color;
        //}
        if (fadingOut) {
            opacity -= Time.fixedDeltaTime * fadingSpeed;

            if (opacity <= 0) {
                fadingOut = false;
                opacity = 0;
                Time.timeScale = 1;
                openSign.SetActive(false);
                // Start game
            }

            color.a = opacity;
            spriteRenderer.color = color;
        }
    }

    public void StartFadeOut() {
        Time.timeScale = 0;
        fadingOut = true;
    }

    //public void StartFadeIn() {
    //    Time.timeScale = 0;
    //    fadingIn = true;
    //}
}
