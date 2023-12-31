using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class BurgerActivity : MonoBehaviour, IActivity
{
    float cookingTimer = 0f; // Current cooking time of the patty
    bool grilled = false;
    int pattyStatus = 0; // 0 no patty, 1 raw patty, 2 cooked patty, 3 burnt patty
    float cookingTime = 5f; // Time to cook the patty
    float burntTime = 10f;
    Grill grill;

    bool readyToServe = false;
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

    public void ClearDisplay()
    {
        grill.DestroyCurrentPatty();
        cookingUI.HideGrillTimer();
        cookingUI.DeactivateButtons();
        cookingUI.HideHud();
    }

    public void ProcessInput()
    {
        if (GameState.gameIsPaused()) {
            return;
        }
        if (!grilled) {
            DisplayGrillTimer();
            UpdateGrill();
            if (Input.GetKeyDown(KeyCode.P)) {
                if (pattyStatus == 0) {
                    cookingTimer = 0;
                    pattyStatus = 1;
                    grill.SetPattyGameObject(pattyStatus);
                    highlightedKeys[0] = true;
                    cookingUI.HighlightButtonBackground(0);
                    cookingUI.HidePanelSpaceBar();
                    cookingUI.DisplayGrillTimer();
                }
            }
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (pattyStatus == 2) {
                    AudioManager.instance.StopPlayingSound();
                    grilled = true;
                    UpdateButtons();
                    grill.DestroyCurrentPatty();
                    cookingUI.HideGrill();
                    cookingUI.HidePanelSpaceBar();
                    cookingUI.HideGrillTimer();
                } else if (pattyStatus == 3) {
                    AudioManager.instance.StopPlayingSound();
                    grill.DestroyCurrentPatty();
                    cookingTimer = 0;
                    pattyStatus = 0;
                    highlightedKeys[0] = false;
                    cookingUI.HideGrill();
                    cookingUI.HideGrillTimer();
                }
            }
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
                }
                else if (!PlayerData.HasEnoughMoney(Recipe.ingredientCost[ingredient])) {
                    continue;
                }
                else if (!controller.IngredientAlreadyAdded(ingredient)) {
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
            if (pattyStatus == 1) {
                grill.SetPattyGameObject(1);
            } else if (pattyStatus == 2) {
                grill.SetPattyGameObject(2);
            } else if (pattyStatus == 3) {
                grill.SetPattyGameObject(3);
            }
        }
    }

    public void ResetDish()
    {
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

    public void SetupDisplay()
    {
        DisplayGrillTimer();
        UpdateGrill();
        UpdateButtons();
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
        if (pattyStatus == 3) {
            cookingUI.SetCookedPattySlider(1);
            cookingUI.SetBurntPattySlider(1);
        }
        else if (pattyStatus == 2) {
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
            cookingUI.SetIngredientImage(PrefabCache.instance.iconDict[Recipe.Ingredients.patty] , 0);
            cookingUI.SetButtonText("P", 0);
            keycodeIndex = new Dictionary<KeyCode, int>
            {
                [KeyCode.P] = 0
            };
            if (highlightedKeys[0]) {
                cookingUI.HighlightButtonBackground(0);
            }
            return;
        }
        
        availableKeys = Recipe.GetCurrentKeys(Recipe.FoodTypes.Burger);
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

    public void DisplayGrill() {
        cookingUI.DisplayGrill();
    }

    public void DestroyActivity()
    {
        Destroy(this);
    }

    public void UpdateActivity(float deltaTime)
    {
        if (pattyStatus == 1 || pattyStatus == 2) { 
            cookingTimer += deltaTime;
            setPattyStage();
        }
    }

    private void setPattyStage() {
        if (cookingTimer <= cookingTime) {
            pattyStatus = 1;
        } else if (cookingTimer <= burntTime) {
            pattyStatus = 2;
        } else {
            pattyStatus = 3;
        }
    }
}
