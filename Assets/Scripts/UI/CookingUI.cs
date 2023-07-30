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
    string currentDishName;
    Dictionary<KeyCode, Recipe.Ingredients> availableKeys;
    DataUI dataUI;
    Cooking cookingScript;
    int currentIndex;

    void Start()
    {
        cookingScript = GameObject.Find("DishPanel").GetComponent<Cooking>();
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

    public void UpdateButtons(string name, int index) {
        DeactivateButtons();
        currentIndex = index;
        currentDishName = name;
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
        availableKeys = null;
    }

    void ProcessInput() {
        if (availableKeys == null) return;
        foreach (KeyCode key in availableKeys.Keys) {
            if (Input.GetKeyDown(key)) {
                Recipe.Ingredients ingredient = availableKeys[key];
                if (!PlayerData.HasEnoughMoney(1)) {
                    continue;
                }
                PlayerData.money -= 1;
                dataUI.UpdateMoneyUI();

                if (Recipe.requiredIngredients[currentDishName].Contains(ingredient)) {
                    cookingScript.MakeIngredientVisible(ingredient);
                } else {
                    Debug.Log("Wrong ingredient. Dish scrapped");
                    cookingScript.ResetDish(currentIndex);
                }
            }
        }
    }
}
