using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaladActivity : MonoBehaviour, IActivity {
    bool addingDressing = false; // Is pizza ready to bake
    float ranchAmount;
    float thousandAmount;
    float vinaigretteAmount;
    float minThreshold = .25f;
    float lessThreshold = 1.5f;
    float regularThreshold = 3f;
    float extraThreshold = 4.5f;
    Dressing dressing;
    int dressingOrdered;
    float addAmount = 0f;

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
        this.dressingOrdered = GenerateRandomDressing();
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
            CheckIfReadyToServe();
            if (readyToServe) {
                cookingUI.UnhighlightButtonBackground(0);
                cookingUI.UnhighlightButtonBackground(1);
                cookingUI.UnhighlightButtonBackground(2);
                cookingUI.DisplaySpaceBar("Press space to server");
                if (Input.GetKey(KeyCode.Space)) {
                    cookingUI.HidePanelSpaceBar();
                    cookingUI.DeactivateButtons();
                    dressing.DestroyCurrentDressing();
                    controller.ResetDishBeingWorkedOn(index);
                    controller.ServeTheDish();
                }
            } else {
                cookingUI.HidePanelSpaceBar();
            }

            if (Input.GetKey(KeyCode.R) && SauceMatchesOrderedDressing("Ranch")) {
                ranchAmount += addAmount;
                cookingUI.HighlightButtonBackground(0);
                dressing.SetCurrentDressingObject(GetDressingStage("Ranch"));
            } else if (!Input.GetKey(KeyCode.R)) {
                cookingUI.UnhighlightButtonBackground(0);
            }
            if (Input.GetKey(KeyCode.T) && SauceMatchesOrderedDressing("Thousand")) {
                thousandAmount += addAmount;
                cookingUI.HighlightButtonBackground(1);
                dressing.SetCurrentDressingObject(GetDressingStage("Thousand"));
            } else if (!Input.GetKey(KeyCode.T)) {
                cookingUI.UnhighlightButtonBackground(1);
            }
            if (Input.GetKey(KeyCode.V) && SauceMatchesOrderedDressing("Vinaigrette")) {
                vinaigretteAmount += addAmount;
                cookingUI.HighlightButtonBackground(2);
                dressing.SetCurrentDressingObject(GetDressingStage("Vinaigrette"));
            } else if (!Input.GetKey(KeyCode.V)) {
                cookingUI.UnhighlightButtonBackground(1);
            }
            DisplayDressingBar();
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
        if (!addingDressing) {
            return;
        }
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
            if (ranchAmount <= lessThreshold && ranchAmount > minThreshold) {
                return 0;
            } else if (ranchAmount <= regularThreshold && ranchAmount > minThreshold) {
                return 1;
            } else if (ranchAmount <= extraThreshold && ranchAmount > minThreshold) {
                return 2;
            } else {
                return 2;
            }
        } else if (type.Equals("Thousand")) {
            if (thousandAmount <= lessThreshold && vinaigretteAmount > minThreshold) {
                return 3;
            } else if (thousandAmount <= regularThreshold && vinaigretteAmount > minThreshold) {
                return 4;
            } else if (thousandAmount <= extraThreshold && vinaigretteAmount > minThreshold) {
                return 5;
            } else {
                return 5;
            }
        } else if (type.Equals("Vinaigrette")) {
            if (vinaigretteAmount <= lessThreshold && vinaigretteAmount > minThreshold) {
                return 6;
            } else if (vinaigretteAmount <= regularThreshold && vinaigretteAmount > minThreshold) {
                return 7;
            } else if (vinaigretteAmount <= extraThreshold && vinaigretteAmount > minThreshold) {
                return 8;
            } else {
                return 8;
            }
        } else {
            return -1;
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
        SetDressingDescription();
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
        switch (GetDressingOrdered()) {
            case "Ranch":
                cookingUI.SetDressingProgressBar(ranchAmount / extraThreshold);
                break;
            case "Thousand":
                cookingUI.SetDressingProgressBar(thousandAmount / extraThreshold);
                break;
            case "Vinaigrette":
                cookingUI.SetDressingProgressBar(vinaigretteAmount / extraThreshold);
                break;
            default:
                return;
        }
    }

    public void DestroyActivity() {
        Destroy(this);
    }

    public void UpdateActivity(float deltaTime) {
        if (addingDressing) {
            addAmount = deltaTime;
        }
        SetDressingDescription();
    }

    public int GenerateRandomDressing() {
        return UnityEngine.Random.Range(0, 9);
    }

    private void CheckIfReadyToServe() {
        cookingUI.HidePanelSpaceBar();
        if (addingDressing) {
            readyToServe = (dressingOrdered == GetDressingStage(GetDressingOrdered()));
        }
    }
}
