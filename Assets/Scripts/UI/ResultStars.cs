using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultStars : MonoBehaviour {
    [SerializeField] SpriteRenderer[] starSprites;
    [SerializeField] Sprite successStar;
    [SerializeField] Sprite failureStar;


    void Start() {
        for (int i = 0; i < starSprites.Length; i++) {
            if (PlayerData.stars[i]) {
                starSprites[i].sprite = successStar;
            } else {
                starSprites[i].sprite = failureStar;
            }
        }
    }
}
