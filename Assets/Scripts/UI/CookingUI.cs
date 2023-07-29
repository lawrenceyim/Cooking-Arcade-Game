using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CookingUI : MonoBehaviour
{
    [SerializeField] GameObject[] cookingSlots;
    [SerializeField] TextMeshPro[] ingredientNames;
    [SerializeField] GameObject serveButton;
    [SerializeField] GameObject restartButton;

    // Start is called before the first frame update
    void Start()
    {
        ingredientNames = new TextMeshPro[cookingSlots.Length];
        for (int i = 0; i < cookingSlots.Length; i++) {
            ingredientNames[i] = cookingSlots[i].transform.Find("Text").GetComponent<TextMeshPro>();
        }
        DeactivateButtons();
    }

    public void UpdateButtons(string name) {
        DeactivateButtons();
        List<string> ingredients = Recipe.GetAllIngredients(name);
        for (int i = 0; i < ingredients.Count; i++) {
            cookingSlots[i].SetActive(true);
            ingredientNames[i].text = ingredients[i];
        }
        serveButton.SetActive(true);
        restartButton.SetActive(true);
    }

    public void DeactivateButtons() {
        serveButton.SetActive(false);
        restartButton.SetActive(false);
        for (int i = 0; i < cookingSlots.Length; i++) {
            ingredientNames[i].text = "";
            cookingSlots[i].SetActive(false);
        }
    }

    public OrderManager.CookingUIDelegate GetCookingDelegate() {
        return UpdateButtons;
    }

    public OrderManager.CookingUIResetDelegate GetCookingResetDelegate() {
        return DeactivateButtons;
    }
}
