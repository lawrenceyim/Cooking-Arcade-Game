using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenSign : MonoBehaviour {
    [SerializeField] List<TextMeshPro> texts;

    void Start() {
        int[] goals = DailyProfitGoal.GetDailyProfitGoal(PlayerData.day);
        for (int i = 0; i < goals.Length; i++) {
            texts[i].text = goals[i].ToString();
        }
    }
}
