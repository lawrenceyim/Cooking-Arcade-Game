using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CookingUI : MonoBehaviour
{
    [SerializeField] GameObject[] cookingSlots;
    [SerializeField] TextMeshPro[] ingredientNames;

    // Start is called before the first frame update
    void Start()
    {
        ingredientNames = new TextMeshPro[cookingSlots.Length];
        for (int i = 0; i < cookingSlots.Length; i++) {
            ingredientNames[i] = cookingSlots[i].transform.Find("Text").GetComponent<TextMeshPro>();
            cookingSlots[i].SetActive(false);
        }
    }

    public void UpdateButtons(string name) {
        List<string> ingredients = Recipe.GetAllIngredients(name);
        for (int i = 0; i < ingredients.Count; i++) {
            cookingSlots[i].SetActive(true);
            ingredientNames[i].text = ingredients[i];
        }
    }

    public void ResetButtons() {
        for (int i = 0; i < cookingSlots.Length; i++) {
            cookingSlots[i].SetActive(false);
        }
    }

    public OrderManager.CookingUIDelegate GetCookingDelegate() {
        return UpdateButtons;
    }

    public OrderManager.CookingUIResetDelegate GetCookingResetDelegate() {
        return ResetButtons;
    }
}
