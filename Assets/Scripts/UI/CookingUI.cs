using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CookingUI : MonoBehaviour
{
    [SerializeField] GameObject[] cookingSlots;
    [SerializeField] TextMeshPro[] ingredientNames;
    [SerializeField] TextMeshPro[] buttonKeys;
    [SerializeField] GameObject serveButton;
    [SerializeField] GameObject restartButton;
    Recipe.FoodTypes currentFoodtype;
    Dictionary<KeyCode, Recipe.Ingredients> availableKeys;
    DataUI dataUI;
    void Start()
    {
        dataUI = GameObject.Find("DataPanel").GetComponent<DataUI>();
        availableKeys = new Dictionary<KeyCode, Recipe.Ingredients>();
        ingredientNames = new TextMeshPro[cookingSlots.Length];
        buttonKeys = new TextMeshPro[cookingSlots.Length];
        for (int i = 0; i < cookingSlots.Length; i++) {
            ingredientNames[i] = cookingSlots[i].transform.Find("Text").GetComponent<TextMeshPro>();
            buttonKeys[i] = cookingSlots[i].transform.Find("Button").transform.Find("Text").GetComponent<TextMeshPro>();
        }
        DeactivateButtons();
    }

    void Update() {
        ProcessInput();
    }

    public void UpdateButtons(string name) {
        DeactivateButtons();
        currentFoodtype = Recipe.foodTypes[name];
        availableKeys = Recipe.GetCurrentKeys(currentFoodtype);
        List<Recipe.Ingredients> ingredients = Recipe.GetAllIngredients(name);
        for (int i = 0; i < ingredients.Count; i++) {
            cookingSlots[i].SetActive(true);
            ingredientNames[i].text = ingredients[i].ToString();
            buttonKeys[i].text = Recipe.keyMapping[ingredients[i]].ToString();
        }
        serveButton.SetActive(true);
        restartButton.SetActive(true);
    }

    public void DeactivateButtons() {
        serveButton.SetActive(false);
        restartButton.SetActive(false);
        for (int i = 0; i < cookingSlots.Length; i++) {
            ingredientNames[i].text = "";
            buttonKeys[i].text = "";
            cookingSlots[i].SetActive(false);
        }
    }

    void ProcessInput() {
        foreach (KeyCode key in availableKeys.Keys) {
            if (Input.GetKeyDown(key)) {
                Recipe.Ingredients ingredient = availableKeys[key];
                if (!PlayerData.HasEnoughMoney(10)) {
                    continue;
                }
                PlayerData.money -= 10;
                dataUI.UpdateMoneyUI();
                // Add sprite
                // Add ingredient to the dish in code
            
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
            // Serve
            // Calculate sales
            // Call customer to leave
        }
        if (Input.GetKeyDown(KeyCode.Backspace)) {
            // Reset the dish in code
            // Reset sprites for the dish
        }
    }

    void SetButtonVisual(int buttonIndex) {
        if (currentFoodtype == Recipe.FoodTypes.Burger) {

        } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {

        } else if (currentFoodtype == Recipe.FoodTypes.Salad) {

        }
    }
}
