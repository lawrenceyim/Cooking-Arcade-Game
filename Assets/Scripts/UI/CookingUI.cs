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
    SpriteRenderer[] buttonSpriteRenderers;
    TextMeshPro[] buttonKeys;
    Dictionary<KeyCode, Recipe.Ingredients> availableKeys;
    int currentIndex;
    List<Recipe.Ingredients> neededForDish;
    Dish dish;
    Dictionary<KeyCode, int> keycodeIndex;
    bool[,] grayKeys;
    [SerializeField] Color originalColor;


    void Start()
    {
        if (controller == null) {
            Debug.LogError("controller script is null");
        }
        availableKeys = new Dictionary<KeyCode, Recipe.Ingredients>();
        buttonKeys = new TextMeshPro[cookingSlots.Length];
        buttonSpriteRenderers = new SpriteRenderer[cookingSlots.Length];
        grayKeys = new bool[6, cookingSlots.Length];
        for (int i = 0; i < cookingSlots.Length; i++) {
            buttonSpriteRenderers[i] = cookingSlots[i].GetComponent<SpriteRenderer>();
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
        keycodeIndex = new Dictionary<KeyCode, int>();
        for (int i = 0; i < ingredients.Count; i++) {
            buttonSpriteRenderers[i].color = Color.red;
            iconSpriteRenderers[i].sprite = PrefabCache.instance.iconDict[ingredients[i]];
            iconSpriteRenderers[i].enabled = true;
            buttonKeys[i].text = Recipe.keyMapping[ingredients[i]].ToString();
            keycodeIndex.Add(Recipe.keyMapping[ingredients[i]], i);
            if (grayKeys[currentIndex, i]) {
                buttonSpriteRenderers[i].color = Color.gray;
            }
        }
        neededForDish = dish.ingredientsList;
    }

    public void DeactivateButtons() {
        for (int i = 0; i < cookingSlots.Length; i++) {
            iconSpriteRenderers[i].enabled = false;
            buttonKeys[i].text = "";
            buttonSpriteRenderers[i].color = originalColor;
        }
        availableKeys = null;
    }

    void ProcessInput() {
        if (availableKeys == null) return;
        if (controller.IsDishComplete()) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                for (int i = 0; i < cookingSlots.Length; i++) {
                    grayKeys[currentIndex, i] = false;
                }
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
                    for (int i = 0; i < availableKeys.Count; i++) {
                        buttonSpriteRenderers[i].color = Color.red;
                        grayKeys[currentIndex, i] = false;
                    }
                } 
                else if (!PlayerData.HasEnoughMoney(Recipe.ingredientCost[ingredient])) {
                    continue;
                }
                else if (!controller.IngredientAlreadyAdded(ingredient)) {
                    AudioManager.instance.PlayAddIngredientSound();
                    PlayerData.money -= Recipe.ingredientCost[ingredient];
                    controller.UpdateMoneyUI();
                    controller.AddIngredientToDish(ingredient);
                    buttonSpriteRenderers[keycodeIndex[key]].color = Color.gray;
                    grayKeys[currentIndex, keycodeIndex[key]] = true;
                    continue;
                } 
            }
        }
    }
}
