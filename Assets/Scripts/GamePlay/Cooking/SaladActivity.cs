using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaladActivity : MonoBehaviour, IActivity {
    bool addingDressing = false; // Is pizza ready to bake
    float ranchAmount;
    float thousandAmount;
    float vinaigretteAmount;
    float lessThreshold = 0.5f;
    float regularThreshold = 1.5f;
    float extraThreshold = 2.5f;
    Dressing dressing;
    int dressingOrdered;

    bool readyToServe = false;
    int currentIngredient = 0;
    CookingUI cookingUI;
    Controller controller;
    Dictionary<KeyCode, Recipe.Ingredients> availableKeys = Recipe.GetCurrentKeys(Recipe.FoodTypes.Burger);
    Dictionary<KeyCode, int> keycodeIndex;
    bool[] highlightedKeys;
    List<Recipe.Ingredients> neededForDish;
    Dish dish;
    int index;

    public SaladActivity(CookingUI cookingUI, Controller controller, Dish dish, int index, Dressing dressing) {
        this.cookingUI = cookingUI;
        this.controller = controller;
        this.dish = dish;
        this.index = index;
        highlightedKeys = new bool[cookingUI.GetCookingSlotsLength()];
        neededForDish = dish.ingredientsList;
        this.dressing = dressing;
    }

    public void ClearDisplay() {
        dressing.DestroyCurrentDressing();
        cookingUI.HideDressingProgressBar();
        cookingUI.DeactivateButtons();
        cookingUI.HideHud();
    }

    public void ProcessInput() {
        if (GameState.gameIsPaused()) {
            return;
        }
        if (addingDressing) {
            if (readyToServe) {
                cookingUI.UnhighlightButtonBackground(0);
                cookingUI.UnhighlightButtonBackground(1);
                cookingUI.UnhighlightButtonBackground(2);
                cookingUI.DisplaySpaceBar("Press space to server");
                if (Input.GetKey(KeyCode.Space)) {
                    controller.ServeTheDish();
                }
                return;
            } else {
                cookingUI.HidePanelSpaceBar();
            }

            if (Input.GetKey(KeyCode.R) && SauceMatchesOrderedDressing("Ranch")) {
                cookingUI.HighlightButtonBackground(0);
                dressing.SetCurrentDressingObject(GetDressingStage("Ranch"));
            } else if (!Input.GetKey(KeyCode.R)) {
                cookingUI.UnhighlightButtonBackground(0);
            }
            if (Input.GetKey(KeyCode.T) && SauceMatchesOrderedDressing("Thousand")) {
                cookingUI.HighlightButtonBackground(1);
                dressing.SetCurrentDressingObject(GetDressingStage("Thousand"));
            } else if (!Input.GetKey(KeyCode.T)) {
                cookingUI.UnhighlightButtonBackground(1);
            }
            if (Input.GetKey(KeyCode.V) && SauceMatchesOrderedDressing("Vinaigrette")) {
                cookingUI.HighlightButtonBackground(2);
                dressing.SetCurrentDressingObject(GetDressingStage("Vinaigrette"));
            } else if (!Input.GetKey(KeyCode.V)) {
                cookingUI.UnhighlightButtonBackground(1);
            }

            // SET CURRENT DRESSING OBJECT
            return;
        }

        if (readyToServe) {
            cookingUI.DisplaySpaceBar("Press space to server");
            if (Input.GetKeyDown(KeyCode.Space)) {
                cookingUI.HidePanelSpaceBar();
                cookingUI.DeactivateButtons();
                controller.ServeTheDish();
                cookingUI.RemoveActivity(index);
            }
            return;
        }

        foreach (KeyCode key in availableKeys.Keys) {
            if (Input.GetKeyDown(key)) {
                Recipe.Ingredients ingredient = availableKeys[key];
                if (!neededForDish.Contains(ingredient)) {
                    DiscardDish();
                } else if (ingredient != dish.ingredientsList[currentIngredient]) {
                    DiscardDish();
                } else if (!PlayerData.HasEnoughMoney(Recipe.ingredientCost[ingredient])) {
                    continue;
                } else if (!controller.IngredientAlreadyAdded(ingredient)) {
                    AudioManager.instance.PlayAddIngredientSound();
                    PlayerData.DecreaseMoney(Recipe.ingredientCost[ingredient]);
                    controller.UpdateMoneyUI();
                    controller.AddIngredientToDish(ingredient);
                    cookingUI.HighlightButtonBackground(keycodeIndex[key]);
                    highlightedKeys[keycodeIndex[key]] = true;
                    currentIngredient++;
                    continue;
                }
            }
        }

        if (currentIngredient == neededForDish.Count && !addingDressing) {
            cookingUI.DisplaySpaceBar("Press space to go to the dressings.");
            if (Input.GetKeyDown(KeyCode.Space)) {
                addingDressing = true;
                UpdateButtons();
            }
        }
    }

    public void SetDressingDescription() {
        if (dressingOrdered == 0) {
            controller.SetDescription("Little bit of Ranch");
        } else if (dressingOrdered == 1) {
            controller.SetDescription("Moderate amount of Ranch");
        } else if (dressingOrdered == 2) {
            controller.SetDescription("Lot of Ranch");
        } else if (dressingOrdered == 3) {
            controller.SetDescription("Little bit of Thousand Island sauce");
        } else if (dressingOrdered == 4) {
            controller.SetDescription("Moderate amount of Thousand Island sauce");
        } else if (dressingOrdered == 5) {
            controller.SetDescription("Lot of Thousand Island sauce");
        } else if (dressingOrdered == 6) {
            controller.SetDescription("Little bit of Vinaigrette");
        } else if (dressingOrdered == 7) {
            controller.SetDescription("Moderate amount of Vinaigrette");
        } else if (dressingOrdered == 8) {
            controller.SetDescription("Lot of Vinaigrette");
        }
    }

    private int GetDressingStage(String type) {
        if (type.Equals("Ranch")) {
            if (ranchAmount <= lessThreshold && ranchAmount > 0) {
                return 0;
            } else if (ranchAmount <= regularThreshold) {
                return 1;
            } else {
                return 2;
            }
        } else if (type.Equals("Thousand")) {
            if (thousandAmount <= lessThreshold && vinaigretteAmount > 0) {
                return 3;
            } else if (thousandAmount <= regularThreshold) {
                return 4;
            } else {
                return 5;
            }
        } else {
            if (vinaigretteAmount <= lessThreshold && vinaigretteAmount > 0) {
                return 6;
            } else if (vinaigretteAmount <= regularThreshold) {
                return 7;
            } else {
                return 8;
            }
        }
    }

    public bool SauceMatchesOrderedDressing(string sauce) {
        return sauce == GetDressingOrdered();
    }

    public string GetDressingOrdered() {
        if (dressingOrdered <= 2) return "Ranch";
        else if (dressingOrdered <= 5) return "Thousand";
        else return "Vinaigrette";
    }

    public void SetupDisplay() {
        UpdateButtons();
        UpdateDressing();
    }

    public void ResetDish() {
        currentIngredient = 0;
        controller.ResetDishBeingWorkedOn(index);
        for (int i = 0; i < availableKeys.Count; i++) {
            cookingUI.UnhighlightButtonBackground(i);
            highlightedKeys[i] = false;
        }
    }

    private void DiscardDish() {
        AudioManager.instance.PlayTrashSound();
        ResetDish();
    }

    public void UpdateButtons() {
        cookingUI.DisplayHud();
        if (addingDressing) {
            availableKeys = new Dictionary<KeyCode, Recipe.Ingredients>();
            availableKeys[KeyCode.R] = Recipe.Ingredients.Ranch;
            availableKeys[KeyCode.T] = Recipe.Ingredients.ThousandIsland;
            availableKeys[KeyCode.V] = Recipe.Ingredients.Vinaigrette;
            cookingUI.SetIngredientImage(PrefabCache.instance.iconDict[Recipe.Ingredients.Ranch], 0);
            cookingUI.SetIngredientImage(PrefabCache.instance.iconDict[Recipe.Ingredients.ThousandIsland], 1);
            cookingUI.SetIngredientImage(PrefabCache.instance.iconDict[Recipe.Ingredients.Vinaigrette], 0);
            cookingUI.UnhighlightButtonBackground(0);
            cookingUI.UnhighlightButtonBackground(1);
            cookingUI.UnhighlightButtonBackground(2);
            cookingUI.SetButtonText("R", 0);
            cookingUI.SetButtonText("T", 1);
            cookingUI.SetButtonText("V", 2);
            return;
        }
        keycodeIndex = new Dictionary<KeyCode, int>();
        availableKeys = Recipe.GetCurrentKeys(Recipe.FoodTypes.Salad);
        for (int i = 0; i < neededForDish.Count; i++) {
            cookingUI.SetIngredientImage(PrefabCache.instance.iconDict[neededForDish[i]], i);
            cookingUI.UnhighlightButtonBackground(i);
            cookingUI.SetButtonText(Recipe.keyMapping[neededForDish[i]].ToString(), i);
            if (highlightedKeys[i]) {
                cookingUI.HighlightButtonBackground(i);
            }
            keycodeIndex.Add(Recipe.keyMapping[neededForDish[i]], i);
        }
    }

    private void UpdateDressing() {

    }

    private void DisplayDressingBar() {

    }

    public void DestroyActivity() {
        Destroy(this);
    }

    public void UpdateActivity(float deltaTime) {

    }
}
