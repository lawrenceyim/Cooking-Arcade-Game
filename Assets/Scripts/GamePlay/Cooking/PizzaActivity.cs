using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PizzaActivity : MonoBehaviour, IActivity {
    bool baking = false; // Is pizza ready to bake
    float cookingTimer = 0f; // Current cooking time of the patty
    int pizzaStatus = 0; // 0 no pizza, 1 raw pizza, 2 cooked pizza, 3 burnt pizza
    float cookingTime = 5f;
    float burntTime = 10f;
    Oven oven;

    bool readyToServe = false;
    int currentIngredient = 0;
    CookingUI cookingUI = null;
    Controller controller = null;
    Dictionary<KeyCode, Recipe.Ingredients> availableKeys = Recipe.GetCurrentKeys(Recipe.FoodTypes.Pizza);
    Dictionary<KeyCode, int> keycodeIndex = null;
    bool[] highlightedKeys;
    List<Recipe.Ingredients> neededForDish = null;
    Dish dish = null;
    int index;

    public PizzaActivity(CookingUI cookingUI, Controller controller, Dish dish, int index, Oven oven) {
        this.cookingUI = cookingUI;
        this.controller = controller;
        this.dish = dish;
        this.index = index;
        highlightedKeys = new bool[cookingUI.GetCookingSlotsLength()];
        neededForDish = dish.ingredientsList;
        this.oven = oven;
    }

    public void ClearDisplay() {
        oven.DestroyCurrentPizza();
        cookingUI.HideOvenTimer();
        cookingUI.DeactivateButtons();
        cookingUI.HideHud();
    }

    public void ProcessInput() {
        if (GameState.IsGameIsPaused()) {
            return;
        }

        if (baking) {
            DisplayOvenTimer();
            UpdateOven();
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (pizzaStatus == 2) {
                    oven.DestroyCurrentPizza();
                    cookingUI.HideOven();
                    cookingUI.HidePanelSpaceBar();
                    cookingUI.DeactivateButtons();
                    cookingUI.HideOvenTimer();
                    controller.ServeTheDish();
                } else if (pizzaStatus == 3) {
                    oven.DestroyCurrentPizza();
                    cookingTimer = 0;
                    pizzaStatus = 0;
                    highlightedKeys[0] = false;
                    cookingUI.HideOven();
                    cookingUI.HideOvenTimer();
                } else if (pizzaStatus == 0) {
                    pizzaStatus = 1;
                    oven.SetPizzaGameObject(pizzaStatus);
                    cookingUI.HidePanelSpaceBar();
                    cookingUI.DisplayOvenTimer();
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

        if (currentIngredient == neededForDish.Count && !baking) {
            cookingUI.DisplaySpaceBar("Press space to go to the oven");
            if (Input.GetKeyDown(KeyCode.Space)) {
                baking = true;
                ResetDish();
                pizzaStatus = 0;
                cookingUI.HideHud();
                cookingUI.DisplayOvenTimer();
                cookingUI.DisplayOven();
            }
        }
    }

    private void UpdateOven() {
        if (!baking) {
            AudioManager.instance.StopPlayingSound();
        } else if (baking) {
            DisplayOven();
            switch (pizzaStatus) {
                case 0:
                    cookingUI.DisplaySpaceBar("Press space to put the pizza in the oven");
                    return;
                case 1: case 2: case 3:
                    oven.SetPizzaGameObject(pizzaStatus);
                    return;
            }
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
        DisplayOvenTimer();
        UpdateOven();
        UpdateButtons();
    }

    public void DisplayOvenTimer() {
        if (!baking) {
            cookingUI.HideOvenTimer();
            return;
        }
        if (pizzaStatus == 0) {
            cookingUI.HideOvenTimer();
            return;
        }
        cookingUI.DisplayOvenTimer();
        if (pizzaStatus == 3) {
            cookingUI.SetCookedPizzaSlider(1);
            cookingUI.SetBurntPizzaSlider(1);
        } else if (pizzaStatus == 2) {
            cookingUI.SetCookedPizzaSlider(1);
            cookingUI.SetBurntPizzaSlider((cookingTimer - cookingTime) / cookingTime);
        } else if (pizzaStatus == 1) {
            cookingUI.SetCookedPizzaSlider(cookingTimer / cookingTime);
            cookingUI.SetBurntPizzaSlider(0);
        }
    }

    public void UpdateButtons() {
        if (baking) {
            cookingUI.HideIngredientCard();
            cookingUI.DeactivateButtons();
            switch (pizzaStatus) {
                case 0:
                    cookingUI.DisplaySpaceBar("Press space to put pizza in the oven");
                    break;
                case 1:
                    break;
                case 2:
                    cookingUI.DisplaySpaceBar("Press space to put serve pizza");
                    break;
                case 3:
                    cookingUI.DisplaySpaceBar("Press space to discard burnt pizza");
                    break;
                default:
                    cookingUI.DisplaySpaceBar("Press space to discard burnt pizza");
                    break;
            }
            return;
        }

        availableKeys = new Dictionary<KeyCode, Recipe.Ingredients>();
        keycodeIndex = new Dictionary<KeyCode, int>();
        cookingUI.DisplayHud();

        availableKeys = Recipe.GetCurrentKeys(Recipe.FoodTypes.Pizza);
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

    public void DestroyActivity() {
        Destroy(this);
    }

    public void UpdateActivity(float deltaTime) {
        if (pizzaStatus == 1 || pizzaStatus == 2) {
            cookingTimer += deltaTime;
            SetPizzaStage();
        }
    }

    private void SetPizzaStage() {
        if (cookingTimer <= cookingTime) {
            pizzaStatus = 1;
        } else if (cookingTimer <= burntTime) {
            pizzaStatus = 2;
        } else {
            pizzaStatus = 3;
        }
    }

    public void DisplayOven() {
        cookingUI.DisplayOven();
    }
}
