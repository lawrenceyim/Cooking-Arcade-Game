using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CookingUI : MonoBehaviour
{   
    [SerializeField] Controller controller;
    [SerializeField] GameObject[] cookingSlots;
    [SerializeField] SpriteRenderer[] iconSpriteRenderers;
    TextMeshPro[] buttonKeys;
    Dictionary<KeyCode, Recipe.Ingredients> availableKeys;
    int currentIndex;
    List<Recipe.Ingredients> neededForDish;
    Dish dish;

    void Start()
    {
        if (controller == null) {
            Debug.LogError("controller script is null");
        }
        availableKeys = new Dictionary<KeyCode, Recipe.Ingredients>();
        buttonKeys = new TextMeshPro[cookingSlots.Length];
        for (int i = 0; i < cookingSlots.Length; i++) {
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
            iconSpriteRenderers[i].sprite = PrefabCache.instance.iconDict[ingredients[i]];
            iconSpriteRenderers[i].enabled = true;
            buttonKeys[i].text = Recipe.keyMapping[ingredients[i]].ToString();
        }
        neededForDish = dish.ingredientsList;
    }

    public void DeactivateButtons() {
        for (int i = 0; i < cookingSlots.Length; i++) {
            iconSpriteRenderers[i].enabled = false;
            buttonKeys[i].text = "";
        }
        availableKeys = null;
    }

    void ProcessInput() {
        if (availableKeys == null) return;
        if (controller.IsDishComplete()) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                controller.ServeTheDish();
            }
            return;
        }
        foreach (KeyCode key in availableKeys.Keys) {
            if (Input.GetKeyDown(key)) {
                Recipe.Ingredients ingredient = availableKeys[key];
                if (!neededForDish.Contains(ingredient)) {
                    AudioManager.instance.PlayTrashSound();
                    Debug.Log("Wrong ingredient. Dish scrapped");
                    controller.ResetDishBeingWorkedOn(currentIndex);
                } 
                if (!PlayerData.HasEnoughMoney(Recipe.ingredientCost[ingredient])) {
                    continue;
                }
                if (!controller.IngredientAlreadyAdded(ingredient)) {
                    AudioManager.instance.PlayAddIngredientSound();
                    PlayerData.money -= Recipe.ingredientCost[ingredient];
                    controller.UpdateMoneyUI();
                    controller.AddIngredientToDish(ingredient);
                    continue;
                } 
            }
        }
    }
}
