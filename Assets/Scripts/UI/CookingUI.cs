using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CookingUI : MonoBehaviour
{
    [SerializeField] GameObject[] cookingSlots;
    GameObject[] iconGameObjects;
    TextMeshPro[] buttonKeys;
    Dictionary<KeyCode, Recipe.Ingredients> availableKeys;
    DataUI dataUI;
    Cooking cookingScript;
    int currentIndex;
    List<Recipe.Ingredients> neededForDish;
    Dish dish;

    void Start()
    {
        cookingScript = GameObject.Find("DishPanel").GetComponent<Cooking>();
        dataUI = GameObject.Find("DataPanel").GetComponent<DataUI>();
        availableKeys = new Dictionary<KeyCode, Recipe.Ingredients>();
        iconGameObjects = new GameObject[cookingSlots.Length];
        buttonKeys = new TextMeshPro[cookingSlots.Length];
        for (int i = 0; i < cookingSlots.Length; i++) {
            iconGameObjects[i] = cookingSlots[i].transform.Find("Icon").gameObject;
            buttonKeys[i] = cookingSlots[i].transform.Find("Button").transform.Find("Text").GetComponent<TextMeshPro>();
        }
        DeactivateButtons();
    }

    void Update() {
        ProcessInput();
    }

    public void UpdateButtons(Dish dish, int index) {
        DeactivateButtons();
        this.dish = dish;
        currentIndex = index;
        availableKeys = Recipe.GetCurrentKeys(dish.foodType);
        List<Recipe.Ingredients> ingredients = Recipe.GetAllIngredients(dish);
        for (int i = 0; i < ingredients.Count; i++) {
            cookingSlots[i].SetActive(true);
            iconGameObjects[i].GetComponent<SpriteRenderer>().sprite = PrefabCache.instance.iconDict[ingredients[i]];
            iconGameObjects[i].SetActive(true);
            buttonKeys[i].text = Recipe.keyMapping[ingredients[i]].ToString();
        }
        neededForDish = dish.ingredientsList;
    }

    public void DeactivateButtons() {
        for (int i = 0; i < cookingSlots.Length; i++) {
            iconGameObjects[i].SetActive(false);
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
