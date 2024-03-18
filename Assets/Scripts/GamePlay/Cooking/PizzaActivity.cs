using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PizzaActivity : IActivity {
    enum PizzaStatus {
        NO_PIZZA = 0,
        UNCOOKED_PIZZA = 1,
        COOKED_PIZZA = 2,
        BURNT_PIZZA = 3 
    }

    bool baking = false; // Is pizza ready to bake
    float cookingTimer = 0f; // Current cooking time of the patty
    PizzaStatus pizzaStatus = PizzaStatus.NO_PIZZA;
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
        UpdateActiveActivity();
        if (baking) {
            DisplayOvenTimer();
            UpdateOven();
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (pizzaStatus == PizzaStatus.COOKED_PIZZA) {
                    oven.DestroyCurrentPizza();
                    cookingUI.HideOven();
                    cookingUI.HidePanelSpaceBar();
                    cookingUI.DeactivateButtons();
                    cookingUI.HideOvenTimer();
                    controller.ServeTheDish();
                } else if (pizzaStatus == PizzaStatus.BURNT_PIZZA) {
                    oven.DestroyCurrentPizza();
                    cookingTimer = 0;
                    pizzaStatus = PizzaStatus.NO_PIZZA;
                    highlightedKeys[0] = false;
                    cookingUI.HideOven();
                    cookingUI.HideOvenTimer();
                } else if (pizzaStatus == PizzaStatus.NO_PIZZA) {
                    pizzaStatus = PizzaStatus.UNCOOKED_PIZZA;
                    oven.SetPizzaGameObject((int) pizzaStatus);
                    cookingUI.HidePanelSpaceBar();
                    cookingUI.DisplayOvenTimer();
                }
            }
            return;
        }

        if (readyToServe) {
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

        if (currentIngredient == neededForDish.Count && !baking) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                baking = true;
                ResetDish();
                pizzaStatus = PizzaStatus.NO_PIZZA;
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
                case PizzaStatus.NO_PIZZA:
                    cookingUI.DisplaySpaceBar("Press space to put the pizza in the oven");
                    return;
                case PizzaStatus.UNCOOKED_PIZZA: case PizzaStatus.COOKED_PIZZA: case PizzaStatus.BURNT_PIZZA:
                    oven.SetPizzaGameObject((int) pizzaStatus);
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
        if (pizzaStatus == PizzaStatus.NO_PIZZA) {
            cookingUI.HideOvenTimer();
            return;
        }
        cookingUI.DisplayOvenTimer();
        if (pizzaStatus == PizzaStatus.BURNT_PIZZA) {
            cookingUI.SetCookedPizzaSlider(1);
            cookingUI.SetBurntPizzaSlider(1);
        } else if (pizzaStatus == PizzaStatus.COOKED_PIZZA) {
            cookingUI.SetCookedPizzaSlider(1);
            cookingUI.SetBurntPizzaSlider((cookingTimer - cookingTime) / cookingTime);
        } else if (pizzaStatus == PizzaStatus.UNCOOKED_PIZZA) {
            cookingUI.SetCookedPizzaSlider(cookingTimer / cookingTime);
            cookingUI.SetBurntPizzaSlider(0);
        }
    }

    public void UpdateButtons() {
        if (baking) {
            cookingUI.HideIngredientCard();
            cookingUI.DeactivateButtons();
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

    public void UpdateActivity(float deltaTime) {
        if (pizzaStatus == PizzaStatus.UNCOOKED_PIZZA || pizzaStatus == PizzaStatus.COOKED_PIZZA) {
            cookingTimer += deltaTime;
            SetPizzaStage();
        }
    }

    private void SetPizzaStage() {
        if (cookingTimer <= cookingTime) {
            pizzaStatus = PizzaStatus.UNCOOKED_PIZZA;
        } else if (cookingTimer <= burntTime) {
            pizzaStatus = PizzaStatus.COOKED_PIZZA;
        } else {
            pizzaStatus = PizzaStatus.BURNT_PIZZA;
        }
    }

    public void UpdateActiveActivity() {
        if (baking) {
            if (pizzaStatus == PizzaStatus.NO_PIZZA) {
                cookingUI.DisplaySpaceBar("Press space to put pizza in the oven");
            }
            if (pizzaStatus == PizzaStatus.COOKED_PIZZA) {
                cookingUI.DisplaySpaceBar("Press space to stop baking.");
            }
            if (pizzaStatus == PizzaStatus.BURNT_PIZZA) {
                cookingUI.DisplaySpaceBar("Press space to discard the burnt pizza.");
            }
            if (pizzaStatus == PizzaStatus.UNCOOKED_PIZZA) {
                cookingUI.HidePanelSpaceBar();
            }
        } else {
            if (currentIngredient == neededForDish.Count && !baking) {
                cookingUI.DisplaySpaceBar("Press space to go to the oven");
            }
        }
    }

    public void DisplayOven() {
        cookingUI.DisplayOven();
    }
}
