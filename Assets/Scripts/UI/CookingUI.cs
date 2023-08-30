using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CookingUI : MonoBehaviour
{   
    [SerializeField] Controller controller;
    [SerializeField] GameObject[] cookingSlots;
    [SerializeField] GameObject spacebarServerIndicator;
    [SerializeField] GameObject panelGrill;
    Image[] ingredientImages;
    Image[] buttonBackgroundHighlights;
    TextMeshProUGUI[] buttonKeys;
    
    Dictionary<KeyCode, Recipe.Ingredients> availableKeys;
    List<Recipe.Ingredients> neededForDish;
    Dish dish;
    Dictionary<KeyCode, int> keycodeIndex;
    bool[,] highlightedKeys;
    int currentIndex;
    int[] currentIngredient;
    bool[] grilledAlready;

    void Start()
    {
        if (controller == null) {
            Debug.LogError("controller script is null");
        }
        currentIngredient = new int[6];
        availableKeys = new Dictionary<KeyCode, Recipe.Ingredients>();
        buttonKeys = new TextMeshProUGUI[cookingSlots.Length];
        ingredientImages = new Image[cookingSlots.Length];
        buttonBackgroundHighlights = new Image[cookingSlots.Length];
        highlightedKeys = new bool[6, cookingSlots.Length];
        grilledAlready = new bool[6];
        panelGrill.SetActive(false);
        for (int i = 0; i < cookingSlots.Length; i++) {
            buttonBackgroundHighlights[i] = cookingSlots[i].transform.Find("Image - Button Highlight").GetComponent<Image>();
            ingredientImages[i] = cookingSlots[i].transform.Find("Image - Ingredient").GetComponent<Image>();
            buttonKeys[i] = cookingSlots[i].transform.Find("Text (TMP) - Ingredient Keycode").GetComponent<TextMeshProUGUI>();
        }
        spacebarServerIndicator.SetActive(false);
        DeactivateButtons();
    }

    void Update() {
        ProcessInput();
        if (dish == null) return;
        if (dish.foodType == Recipe.FoodTypes.Burger && !grilledAlready[currentIndex]) {
            controller.SetPattyGameObject(currentIndex);
        }
    }

    public void UpdateButtons(Dish dish, int index) {
        panelGrill.SetActive(false);
        spacebarServerIndicator.SetActive(false);
        DeactivateButtons();
        this.dish = dish;
        currentIndex = index;
        controller.SetPattyGameObject(currentIndex);
        if (dish.foodType == Recipe.FoodTypes.Burger) {
            if (!grilledAlready[currentIndex]) {
                panelGrill.SetActive(true);
                SetGrillKeys();
                return;
            }
        }

        availableKeys = Recipe.GetCurrentKeys(dish.foodType);
        List<Recipe.Ingredients> ingredients = Recipe.GetAllIngredients(dish);
        keycodeIndex = new Dictionary<KeyCode, int>();
        for (int i = 0; i < ingredients.Count; i++) {
            ingredientImages[i].sprite = PrefabCache.instance.iconDict[ingredients[i]];
            ingredientImages[i].enabled = true;
            buttonBackgroundHighlights[i].enabled = false;
            buttonKeys[i].text = Recipe.keyMapping[ingredients[i]].ToString();
            keycodeIndex.Add(Recipe.keyMapping[ingredients[i]], i);
            if (highlightedKeys[currentIndex, i]) {
                buttonBackgroundHighlights[i].enabled = true;
            }
        }
        neededForDish = dish.ingredientsList;
    }

    void SetGrillKeys() {
        availableKeys = new Dictionary<KeyCode, Recipe.Ingredients>();
        availableKeys[KeyCode.P] = Recipe.Ingredients.patty;
        ingredientImages[0].sprite = PrefabCache.instance.iconDict[Recipe.Ingredients.patty];
        ingredientImages[0].enabled = true;
        buttonBackgroundHighlights[0].enabled = false;
        buttonKeys[0].text = "P";
        keycodeIndex = new Dictionary<KeyCode, int>();
        keycodeIndex[KeyCode.P] = 0;
        if (highlightedKeys[currentIndex, 0]) buttonBackgroundHighlights[0].enabled = true;
    }

    public void DeactivateButtons() {
        for (int i = 0; i < cookingSlots.Length; i++) {
            ingredientImages[i].enabled = false;
            buttonBackgroundHighlights[i].enabled = false;
            buttonKeys[i].text = "";
        }
        availableKeys = null;
    }

    void ProcessInput() {
        if (dish == null) return;
        if (dish.foodType == Recipe.FoodTypes.Burger && !grilledAlready[currentIndex]) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (controller.GetPattyStatus(currentIndex) == 2) {
                    grilledAlready[currentIndex] = true;
                    controller.SetPattyStatus(currentIndex, 0);
                    controller.DestroyCurrentPatty();
                    panelGrill.SetActive(false);
                    UpdateButtons(dish, currentIndex);
                } else if (controller.GetPattyStatus(currentIndex) == 3) {
                    controller.SetPattyStatus(currentIndex, 0);
                    controller.DestroyCurrentPatty();
                    highlightedKeys[currentIndex, 0] = false;
                    buttonBackgroundHighlights[0].enabled = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.P) && !highlightedKeys[currentIndex, 0]) {
                controller.AddPattyToGrill(currentIndex);
                controller.SetPattyGameObject(currentIndex);
                highlightedKeys[currentIndex, 0] = true;
                buttonBackgroundHighlights[0].enabled = true;
            }
            return;
        }

        if (availableKeys == null) return;
        if (controller.IsDishComplete()) {
            spacebarServerIndicator.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space)) {
                grilledAlready[currentIndex] = false;
                spacebarServerIndicator.SetActive(false);
                for (int i = 0; i < cookingSlots.Length; i++) {
                    highlightedKeys[currentIndex, i] = false;
                }
                currentIngredient[currentIndex] = 0;
                controller.ServeTheDish();
            }
            return;
        }
        foreach (KeyCode key in availableKeys.Keys) {
            if (Input.GetKeyDown(key)) {
                Recipe.Ingredients ingredient = availableKeys[key];
                if (!neededForDish.Contains(ingredient)) {
                    ResetDishAndButtons();
                } else if (ingredient != dish.ingredientsList[currentIngredient[currentIndex]]) {
                    ResetDishAndButtons();
                }
                else if (!PlayerData.HasEnoughMoney(Recipe.ingredientCost[ingredient])) {
                    continue;
                }
                else if (!controller.IngredientAlreadyAdded(ingredient)) {
                    AudioManager.instance.PlayAddIngredientSound();
                    PlayerData.DecreaseMoney(Recipe.ingredientCost[ingredient]);
                    controller.UpdateMoneyUI();
                    controller.AddIngredientToDish(ingredient);
                    buttonBackgroundHighlights[keycodeIndex[key]].enabled = true;
                    highlightedKeys[currentIndex, keycodeIndex[key]] = true;
                    currentIngredient[currentIndex]++;
                    continue;
                } 
            }
        }
    }

    private void ResetDishAndButtons() {
        AudioManager.instance.PlayTrashSound();
        currentIngredient[currentIndex] = 0;
        controller.ResetDishBeingWorkedOn(currentIndex);
        for (int i = 0; i < availableKeys.Count; i++) {
            buttonBackgroundHighlights[i].enabled = false;
            highlightedKeys[currentIndex, i] = false;
        }
    }

    public void ResetGrill(int index) {
        grilledAlready[index] = false;
    }
}
