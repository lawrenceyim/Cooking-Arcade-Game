using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndOfDayUI : MonoBehaviour {
    [SerializeField] GameObject summaryPanel;
    [SerializeField] TextMeshPro summaryRevenueExpenseText;
    [SerializeField] TextMeshPro summaryProfitText;
    [SerializeField] TextMeshPro dayText;
    [SerializeField] SpriteRenderer[] starSprites; // Should only be exactly 3 sprites
    [SerializeField] Sprite successSprite;
    [SerializeField] Sprite failureSprite;
    [SerializeField] CookingUI cookingUI;

    [SerializeField] SpriteRenderer startNextDaySpriteRenderer;
    [SerializeField] BoxCollider2D startNextDayBoxCollider;
    Color availableColor = new Color(255, 255, 255, 1f);
    Color disabledColor = new Color(0, 0, 0, .5f);

    private void Start() {
        summaryPanel.SetActive(false);
        foreach (SpriteRenderer spriteRenderer in starSprites) {
            spriteRenderer.sprite = failureSprite;
        }
    }

    public void UpdateSummaryText() {
        cookingUI.HideHud();
        dayText.text = "Day " + PlayerData.day.ToString();
        summaryRevenueExpenseText.text = $"{PlayerData.revenue}\n{-PlayerData.expense}";
        summaryProfitText.text = $"{PlayerData.revenue - PlayerData.expense}";
        summaryPanel.SetActive(true);
        PlayerData.saveFileExists = true;
        CalculatePerformance();
    }

    private void CalculatePerformance() {
        int[] goals = DailyProfitGoal.GetDailyProfitGoal(PlayerData.day);
        int profit = PlayerData.revenue - PlayerData.expense;
        if (profit >= goals[0]) {
            PlayerData.PassedDay(PlayerData.day);
            ChangeStarSprite(0);    
        }
        if (profit >= goals[1]) {
            ChangeStarSprite(1);
        }
        if (profit >= goals[2]) {
            ChangeStarSprite(2);
            PlayerData.AddStarForDay(PlayerData.day);
        }

        if (PlayerData.PlayerPassedDaySuccessfully(PlayerData.day)) {
            PlayerData.IncrementDay();
            startNextDaySpriteRenderer.color = availableColor;
            startNextDayBoxCollider.enabled = true;
        } else {
            startNextDaySpriteRenderer.color = disabledColor;
            startNextDayBoxCollider.enabled = false;
        }
        PlayerData.SaveData();
    }
    private void ChangeStarSprite(int index) {
        starSprites[index].sprite = successSprite;
    }
}