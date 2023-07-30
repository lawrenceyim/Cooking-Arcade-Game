using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CookingUI : MonoBehaviour
{
    [SerializeField] GameObject[] cookingSlots;
    TextMeshPro[] ingredientNames;
    TextMeshPro[] buttonKeys;
    Recipe.FoodTypes currentFoodtype;
    string currentDishName;
    Dictionary<KeyCode, Recipe.Ingredients> availableKeys;
    DataUI dataUI;
    Cooking cookingScript;
    int currentIndex;
    List<Recipe.Ingredients> neededForDish;

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
        neededForDish = Recipe.requiredIngredients[name];
    }

    public void DeactivateButtons() {
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
                if (!neededForDish.Contains(ingredient)) {
                    Debug.Log("Wrong ingredient. Dish scrapped");
                    cookingScript.ResetDish(currentIndex);
                } 
                if (!PlayerData.HasEnoughMoney(Recipe.ingredientCost[ingredient])) {
                    continue;
                }
                if (!cookingScript.IngredientAlreadyAdded(ingredient)) {
                    PlayerData.money -= Recipe.ingredientCost[ingredient];
                    dataUI.UpdateMoneyUI();
                    cookingScript.MakeIngredientVisible(ingredient);
                    continue;
                } 
            }
        }
    }
}
