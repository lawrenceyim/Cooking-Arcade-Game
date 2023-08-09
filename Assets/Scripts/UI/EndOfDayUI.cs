using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndOfDayUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI summaryText;
    [SerializeField] GameObject summaryPanel;

    private void Start() {
        summaryPanel.SetActive(false);
    }

    public void UpdateSummaryText() {
        summaryText.text = $"Revenue: ${PlayerData.revenue}\nExpense: ${-PlayerData.expense}\nProfit: ${PlayerData.revenue + PlayerData.expense}";
        summaryPanel.SetActive(true);
    }
}
