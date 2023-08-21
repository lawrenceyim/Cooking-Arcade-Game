using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndOfDayUI : MonoBehaviour
{
    [SerializeField] GameObject summaryPanel;
    [SerializeField] TextMeshProUGUI summaryAmountText;
    [SerializeField] TextMeshProUGUI profitAmountText;


    private void Start() {
        summaryPanel.SetActive(false);
    }

    public void UpdateSummaryText() {
        summaryAmountText.text = $"{PlayerData.revenue}\n{-PlayerData.expense}";
        profitAmountText.text = $"{PlayerData.revenue - PlayerData.expense}";
        summaryPanel.SetActive(true);
    }
}
