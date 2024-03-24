using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndOfDayUI : MonoBehaviour {
    [SerializeField] GameObject summaryPanel;
    [SerializeField] TextMeshPro summaryAmountText;
    [SerializeField] TextMeshPro dayText;
    [SerializeField] SpriteRenderer[] starSprites; // Should only be exactly 3 sprites
    [SerializeField] TextMeshPro buttonText;

    Color failureColor = new Color(0, 0, 0, .2f);
    Color successColor = new Color(255, 255, 255, 1f);


    float oneStarThreshold = 50;
    float twostarThreshold = 150;
    float threeStarThreshold = 300;
    bool daySuccessful = false;

    private void Start() {
        summaryPanel.SetActive(false);
        foreach (SpriteRenderer sprite in starSprites) {
            sprite.color = failureColor;
        }
    }

    public void UpdateSummaryText() {
        dayText.text = "Day " + PlayerData.day.ToString();
        summaryAmountText.text = $"{PlayerData.revenue}\n{-PlayerData.expense}\n\n{PlayerData.revenue - PlayerData.expense}";
        summaryPanel.SetActive(true);
        PlayerData.saveFileExists = true;
        CalculatePerformance();
    }

    private void CalculatePerformance() {
        if (PlayerData.revenue >= oneStarThreshold) {
            daySuccessful = true;
            ChangeStarColor(0);    
        }
        if (PlayerData.revenue >= twostarThreshold) {
            ChangeStarColor(1);
        }
        if (PlayerData.revenue >= threeStarThreshold) {
            ChangeStarColor(2);
        }

        if (daySuccessful) {
            PlayerData.IncrementDay();
        } else {
            buttonText.text = "Retry Day";
        }
        PlayerData.SaveData();
    }

    private void ChangeStarColor(int index) {
        starSprites[index].color = successColor;
    }
}