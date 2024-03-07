using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndOfDayUI : MonoBehaviour {
    [SerializeField] GameObject summaryPanel;
    [SerializeField] TextMeshPro summaryAmountText;
    [SerializeField] TextMeshPro dayText;

    private void Start() {
        summaryPanel.SetActive(false);
    }

    public void UpdateSummaryText() {
        dayText.text = "Day " + PlayerData.day.ToString();
        summaryAmountText.text = $"{PlayerData.revenue}\n{-PlayerData.expense}\n\n{PlayerData.revenue - PlayerData.expense}";
        summaryPanel.SetActive(true);
        PlayerData.IncrementDay();
        PlayerData.SaveData();
    }
}