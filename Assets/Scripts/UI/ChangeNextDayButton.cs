using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeNextDayButton : MonoBehaviour, Observer
{
    [SerializeField] EoDSpriteButton script;
    [SerializeField] TextMeshPro textMeshPro;

    private void Start() {
        if (script == null) {
            Debug.Log("script is null");
        }
        if (textMeshPro == null) {
            Debug.Log("text is null");

        }
    }

    public void ReceiveUpdate()
    {
        if (PlayerData.day >= 21) {
            textMeshPro.text = "Continue";
            script.SetButtonAction(EoDSpriteButton.ButtonAction.EndGame);        
        }
    }
}
