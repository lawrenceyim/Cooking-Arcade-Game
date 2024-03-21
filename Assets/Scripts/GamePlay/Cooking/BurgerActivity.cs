using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BurgerActivity : IActivity {
    enum PattyStatus {
        NO_PATTY = 0,
        RAW_PATTY = 1,
        COOKED_PATTY = 2,
        BURNT_PATTY = 3
    }

    float cookingTimer = 0f; // Current cooking time of the patty
    bool grilled = false;
    PattyStatus pattyStatus = PattyStatus.NO_PATTY;
    float cookingTime = 5f; // Time to cook the patty
    float burntTime = 10f;
    Grill grill;

    int currentIngredient = 0;
    CookingUI cookingUI;
    Controller controller;
    Dictionary<KeyCode, Recipe.Ingredients> availableKeys = Recipe.GetCurrentKeys(Recipe.FoodTypes.Burger);
    Dictionary<KeyCode, int> keycodeIndex = null;
    bool[] highlightedKeys;
    List<Recipe.Ingredients> neededForDish = null;
    Dish dish;
    int index;

    public BurgerActivity(CookingUI cookingUI, Controller controller, Dish dish, int index, Grill grill) {
        this.cookingUI = cookingUI;
        this.controller = controller;
        this.dish = dish;
        this.index = index;
        highlightedKeys = new bool[cookingUI.GetCookingSlotsLength()];
        neededForDish = dish.ingredientsList;
        this.grill = grill;
    }

    public void ClearDisplay() {
        grill.DestroyCurrentPatty();
        cookingUI.HideGrillTimer();
        cookingUI.DeactivateButtons();
        cookingUI.HideHud();
    }

    public void ProcessInput() {
        if (GameState.IsGameIsPaused()) {
            return;
        }
        if (!grilled) {
            DisplayGrillTimer();
            UpdateGrill();
            UpdateActiveActivity();
            if (Input.GetKeyDown(KeyCode.P)) {
                if (pattyStatus == 0) {
                    cookingTimer = 0;
                    pattyStatus = PattyStatus.RAW_PATTY;
                    grill.SetPattyGameObject((int) pattyStatus);
                    highlightedKeys[0] = true;
                    cookingUI.HighlightButtonBackground(0);
                    cookingUI.HidePanelSpaceBar();
                    cookingUI.DisplayGrillTimer();
                }
            }
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (pattyStatus == PattyStatus.COOKED_PATTY) {
                    highlightedKeys[0] = false;
                    AudioManager.instance.StopPlayingSound();
                    grilled = true;
                    UpdateButtons();
                    grill.DestroyCurrentPatty();
                    SetupDisplay();
                    cookingUI.HidePanelSpaceBar();
                } else if (pattyStatus == PattyStatus.BURNT_PATTY) {
                    AudioManager.instance.StopPlayingSound();
                    grill.DestroyCurrentPatty();
                    cookingTimer = 0;
                    pattyStatus = 0;
                    highlightedKeys[0] = false;
                    cookingUI.UnhighlightButtonBackground(0);
                    cookingUI.HideGrill();
                    cookingUI.HideGrillTimer();
                }
            }
            return;
        }

        if (controller.IsDishComplete()) {
            cookingUI.DisplaySpaceBar("Press space to serve");
            if (Input.GetKeyDown(KeyCode.Space)) {
                cookingUI.HidePanelSpaceBar();
                cookingUI.DeactivateButtons();
                controller.ServeTheDish();
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
    }

    private void UpdateGrill() {
        if (grilled) {
            AudioManager.instance.StopPlayingSound();
            return;
        }
        DisplayGrill();
        if (pattyStatus > 0) {
            AudioManager.instance.PlayGrillingSound();
            grill.SetPattyGameObject((int) pattyStatus);
        }
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

    public void SetupDisplay() {
        UpdateButtons();
        if (!grilled) {
            DisplayGrillTimer();
            UpdateGrill();
            controller.SetOrderNameOnly(dish);
        } else {
            cookingUI.HideGrill();
            cookingUI.HideGrillTimer();
            controller.SetOrderDescription(dish);
        }
    }

    public void DisplayGrillTimer() {
        if (grilled) {
            cookingUI.HideGrillTimer();
            return;
        }
        if (pattyStatus == 0) {
            cookingUI.HideGrillTimer();
            return;
        }
        cookingUI.DisplayGrillTimer();
        if (pattyStatus == PattyStatus.BURNT_PATTY) {
            cookingUI.SetCookedPattySlider(1);
            cookingUI.SetBurntPattySlider(1);
        } else if (pattyStatus == PattyStatus.COOKED_PATTY) {
            cookingUI.SetCookedPattySlider(1);
            cookingUI.SetBurntPattySlider((cookingTimer - cookingTime) / cookingTime);
        } else {
            cookingUI.SetCookedPattySlider(cookingTimer / cookingTime);
            cookingUI.SetBurntPattySlider(0);
        }
    }

    public void UpdateButtons() {
        availableKeys = new Dictionary<KeyCode, Recipe.Ingredients>();
        keycodeIndex = new Dictionary<KeyCode, int>();
        cookingUI.DisplayHud();

        if (!grilled) {
            availableKeys[KeyCode.P] = Recipe.Ingredients.patty;
            cookingUI.SetIngredientImage(PrefabCache.instance.iconDict[Recipe.Ingredients.patty], 0);
            cookingUI.SetButtonText("P", 0);
            keycodeIndex = new Dictionary<KeyCode, int> {
                [KeyCode.P] = 0
            };
            if (highlightedKeys[0]) {
                cookingUI.HighlightButtonBackground(0);
            }
            return;
        }

        availableKeys = Recipe.GetCurrentKeys(Recipe.FoodTypes.Burger);
        KeyValuePair<KeyCode, Recipe.Ingredients>[] keyValuePairs = availableKeys.ToArray();
        for (int i = 0; i < keyValuePairs.Length; i++) {
            cookingUI.SetIngredientImage(PrefabCache.instance.iconDict[keyValuePairs[i].Value], i);
            cookingUI.UnhighlightButtonBackground(i);
            cookingUI.SetButtonText(Recipe.keyMapping[keyValuePairs[i].Value].ToString(), i);
            if (highlightedKeys[i]) {
                cookingUI.HighlightButtonBackground(i);
            }
            keycodeIndex.Add(Recipe.keyMapping[keyValuePairs[i].Value], i);
        }
    }

    public void DisplayGrill() {
        cookingUI.DisplayGrill();
    }

    public void UpdateActivity(float deltaTime) {
        if (pattyStatus == PattyStatus.RAW_PATTY || pattyStatus == PattyStatus.COOKED_PATTY) {
            cookingTimer += deltaTime;
            SetPattyStage();
        }
    }

    public void UpdateActiveActivity() {
        if (!grilled) {
            if (pattyStatus == PattyStatus.COOKED_PATTY) {
                cookingUI.DisplaySpaceBar("Press space to stop grilling.");
            }
            if (pattyStatus == PattyStatus.BURNT_PATTY) {
                cookingUI.DisplaySpaceBar("Press space to discard the burnt patty.");
            }
        }
    }

    private void SetPattyStage() {
        if (cookingTimer <= cookingTime) {
            pattyStatus = PattyStatus.RAW_PATTY;
        } else if (cookingTimer <= burntTime) {
            pattyStatus = PattyStatus.COOKED_PATTY;
        } else {
            pattyStatus = PattyStatus.BURNT_PATTY;
        }
    }
}
