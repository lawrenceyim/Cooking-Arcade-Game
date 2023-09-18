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
    [SerializeField] GameObject panelSpaceBar;
    [SerializeField] TextMeshPro panelSpaceBarText;
    [SerializeField] GameObject panelGrill;
    [SerializeField] GameObject grillTimer;
    [SerializeField] GameObject panelOven;
    [SerializeField] GameObject ovenTimer;
    [SerializeField] Slider cookedPattySlider;
    [SerializeField] Slider burntPattySlider;
    [SerializeField] Slider cookedPizzaSlider;
    [SerializeField] Slider burntPizzaSlider;
    [SerializeField] GameObject transparencyPanel;
    [SerializeField] GameObject UIDishCard;
    [SerializeField] GameObject ingredientCard;
    [SerializeField] GameObject dressingProgessBar;
    [SerializeField] Slider dressingSlider;
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
    bool[] readyToBakePizza;
    bool[] dressingReady;
    int[] dressingOrdered;

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
        readyToBakePizza = new bool[6];
        dressingReady = new bool[6];
        dressingOrdered = new int[6];
        HideStations();
        HideHud();
        HideGrillTimer();
        HideDressingProgressBar();
        for (int i = 0; i < cookingSlots.Length; i++) {
            buttonBackgroundHighlights[i] = cookingSlots[i].transform.Find("Image - Button Highlight").GetComponent<Image>();
            ingredientImages[i] = cookingSlots[i].transform.Find("Image - Ingredient").GetComponent<Image>();
            buttonKeys[i] = cookingSlots[i].transform.Find("Text (TMP) - Ingredient Keycode").GetComponent<TextMeshProUGUI>();
        }
        panelSpaceBar.SetActive(false);
        DeactivateButtons();
    }

    void Update() {
        ProcessInput();
        if (dish == null) return;
        ProcessGrilling();
        ProcessBakingPizza();
        ProcessDressing();
    }

    public void ProcessGrilling() {
        if (dish.foodType == Recipe.FoodTypes.Burger && !grilledAlready[currentIndex]) {
            controller.SetPattyGameObject(currentIndex);
            DisplayGrillTimer();
            if (controller.GetPattyStatus(currentIndex) == 0) return;
            AudioManager.instance.PlayGrillingSound();
            if (controller.GetPattyStatus(currentIndex) == 2) {
                DisplaySpaceBar("Press space to take cooked patty off the grill");
            } else if (controller.GetPattyStatus(currentIndex) == 3) {
                DisplaySpaceBar("Press space to discard burnt patty");
            }
        }
    }

    public void ProcessBakingPizza() {
        if (dish.foodType == Recipe.FoodTypes.Pizza && readyToBakePizza[currentIndex]) {
            SetOvenKeys();
            controller.SetPizzaGameObject(currentIndex);
            DisplayOvenTimer();
        }
    }

    public void ProcessDressing() {
        if (dish.foodType == Recipe.FoodTypes.Salad && dressingReady[currentIndex]) {
            DisplayDressingProgressBar();
        }
    }

    public void UpdateButtons(Dish dish, int index) {
        DisplayHud();
        HideStations();
        DeactivateButtons();
        AudioManager.instance.StopPlayingSound();
        this.dish = dish;
        currentIndex = index;
        if (dish.foodType == Recipe.FoodTypes.Burger) {
            if (!grilledAlready[currentIndex]) {
                controller.SetPattyGameObject(currentIndex);
                panelGrill.SetActive(true);
                SetGrillKeys();
                return;
            }
        } else if (dish.foodType == Recipe.FoodTypes.Pizza) {
            if (readyToBakePizza[currentIndex]) {
                controller.SetPizzaGameObject(currentIndex);
                panelOven.SetActive(true);
                SetOvenKeys();
                return;
            }
        } else if (dish.foodType == Recipe.FoodTypes.Salad) {
            if (dressingReady[currentIndex]) {
                SetDressingDescription();
                controller.SetCurrentDressingObject(currentIndex, GetDressingOrdered());
                SetDressingKeys();
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

    void SetDressingKeys() {
        availableKeys = new Dictionary<KeyCode, Recipe.Ingredients>();
        availableKeys[KeyCode.R] = Recipe.Ingredients.Ranch;
        availableKeys[KeyCode.T] = Recipe.Ingredients.ThousandIsland;
        availableKeys[KeyCode.V] = Recipe.Ingredients.Vinaigrette;
        ingredientImages[0].sprite = PrefabCache.instance.iconDict[Recipe.Ingredients.Ranch];
        ingredientImages[1].sprite = PrefabCache.instance.iconDict[Recipe.Ingredients.ThousandIsland];
        ingredientImages[2].sprite = PrefabCache.instance.iconDict[Recipe.Ingredients.Vinaigrette];
        ingredientImages[0].enabled = true;
        ingredientImages[1].enabled = true;
        ingredientImages[2].enabled = true;
        buttonBackgroundHighlights[0].enabled = false;
        buttonBackgroundHighlights[1].enabled = false;
        buttonBackgroundHighlights[2].enabled = false;
        buttonKeys[0].text = "R";
        buttonKeys[1].text = "T";
        buttonKeys[2].text = "V";
    }

    public void SetOvenKeys() {
        ingredientCard.SetActive(false);
        if (controller.GetPizzaStatus(currentIndex) == 1) {
            return;
        }
        if (controller.GetPizzaStatus(currentIndex) == 0) {
            DisplaySpaceBar("Press space to put pizza in the oven");
        } else if (controller.GetPizzaStatus(currentIndex) == 2) {
            DisplaySpaceBar("Press space to put serve pizza");
        } else if (controller.GetPizzaStatus(currentIndex) == 3) {
            DisplaySpaceBar("Press space to discard burnt pizza");
        }

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
                    AudioManager.instance.StopPlayingSound();
                    grilledAlready[currentIndex] = true;
                    controller.SetPattyStatus(currentIndex, 0);
                    controller.DestroyCurrentPatty();
                    panelGrill.SetActive(false);
                    panelSpaceBar.SetActive(false);
                    HideGrillTimer();
                    UpdateButtons(dish, currentIndex);
                } else if (controller.GetPattyStatus(currentIndex) == 3) {
                    AudioManager.instance.StopPlayingSound();
                    controller.SetPattyStatus(currentIndex, 0);
                    controller.DestroyCurrentPatty();
                    highlightedKeys[currentIndex, 0] = false;
                    buttonBackgroundHighlights[0].enabled = false;
                    grillTimer.SetActive(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.P) && !highlightedKeys[currentIndex, 0]) {
                controller.AddPattyToGrill(currentIndex);
                controller.SetPattyGameObject(currentIndex);
                highlightedKeys[currentIndex, 0] = true;
                buttonBackgroundHighlights[0].enabled = true;
                panelSpaceBar.SetActive(false);
                DisplayGrillTimer();
            }
            return;
        }
        if (dish.foodType == Recipe.FoodTypes.Pizza && readyToBakePizza[currentIndex]) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (controller.GetPizzaStatus(currentIndex) == 0) {
                    controller.AddPizzaToOven(currentIndex);
                    controller.SetPizzaGameObject(currentIndex);
                    panelSpaceBar.SetActive(false);
                    DisplayOvenTimer();
                } else if (controller.GetPizzaStatus(currentIndex) == 2) {
                    readyToBakePizza[currentIndex] = false;
                    ResetOven(currentIndex);
                    controller.DestroyCurrentPizza();
                    controller.RemovePizzaFromOven(currentIndex);
                    panelOven.SetActive(false);
                    panelSpaceBar.SetActive(false);
                    HideOvenTimer();
                    controller.ServeTheDish();
                } else if (controller.GetPizzaStatus(currentIndex) == 3) {
                    controller.DestroyCurrentPizza();
                    controller.RemovePizzaFromOven(currentIndex);
                    ovenTimer.SetActive(false);
                }
            }
            return;
        }
        if (dish.foodType == Recipe.FoodTypes.Salad && dressingReady[currentIndex]) {
            if (SaladReadyToServe()) {
                buttonBackgroundHighlights[0].enabled = false;
                buttonBackgroundHighlights[1].enabled = false;
                buttonBackgroundHighlights[2].enabled = false;

                DisplaySpaceBar("Press space to serve");
                if (Input.GetKey(KeyCode.Space)) {
                    Debug.Log("Dish servered");
                    controller.ServeTheDish();
                }
                return;
            } else {
                panelSpaceBar.SetActive(false);
            }

            if (Input.GetKey(KeyCode.R) && SauceMatchesOrderedDressing("Ranch")) {
                controller.AddRanch(currentIndex, Time.deltaTime);
                buttonBackgroundHighlights[0].enabled = true;
            } else if (!Input.GetKey(KeyCode.R)) {
                buttonBackgroundHighlights[0].enabled = false;
            } 
            if (Input.GetKey(KeyCode.T) && SauceMatchesOrderedDressing("Thousand")) {
                controller.AddThousand(currentIndex, Time.deltaTime);
                buttonBackgroundHighlights[1].enabled = true;
            } else if (!Input.GetKey(KeyCode.T)) {
                buttonBackgroundHighlights[1].enabled = false;
            }
            if (Input.GetKey(KeyCode.V) && SauceMatchesOrderedDressing("Vinaigrette")) {
                controller.AddVinaigrette(currentIndex, Time.deltaTime);
                buttonBackgroundHighlights[2].enabled = true;
            } else if (!Input.GetKey(KeyCode.V)) {
                buttonBackgroundHighlights[2].enabled = false;
            }
            return;
        }
    
        if (availableKeys == null) return;
        if (controller.IsDishComplete()) {
            if (dish.foodType == Recipe.FoodTypes.Pizza) {
                ResetDishAndButtons();
                readyToBakePizza[currentIndex] = true;
                panelOven.SetActive(true);
                SetOvenKeys();
                controller.ResetDishBeingWorkedOn(currentIndex);
                return;
            }
            if (dish.foodType == Recipe.FoodTypes.Salad && !dressingReady[currentIndex]) {
                SetDressingOrdered(currentIndex);
                DeactivateButtons();
                SetDressingKeys();
                SetDressingDescription();
                dressingReady[currentIndex] = true;
                return;
            }
            DisplaySpaceBar("Press space to serve");
            if (Input.GetKeyDown(KeyCode.Space)) {
                grilledAlready[currentIndex] = false;
                panelSpaceBar.SetActive(false);
                for (int i = 0; i < cookingSlots.Length; i++) {
                    highlightedKeys[currentIndex, i] = false;
                }
                currentIngredient[currentIndex] = 0;
                controller.ServeTheDish();
                return;
            }
            return;
        }
        foreach (KeyCode key in availableKeys.Keys) {
            if (Input.GetKeyDown(key)) {
                Recipe.Ingredients ingredient = availableKeys[key];
                if (!neededForDish.Contains(ingredient)) {
                    DiscardDish();
                } else if (ingredient != dish.ingredientsList[currentIngredient[currentIndex]]) {
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
                    buttonBackgroundHighlights[keycodeIndex[key]].enabled = true;
                    highlightedKeys[currentIndex, keycodeIndex[key]] = true;
                    currentIngredient[currentIndex]++;
                    continue;
                } 
            }
        }
    }

    public void DiscardDish() {
        AudioManager.instance.PlayTrashSound();
        ResetDishAndButtons();
    }

    private void ResetDishAndButtons() {
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

    public void ResetOven(int index) {
        readyToBakePizza[index] = false;
    }

    public void ResetDressing(int index) {
        dressingReady[index] = false;
    }

    public void DisplayHud() {
        transparencyPanel.SetActive(true);
        UIDishCard.SetActive(true);
        ingredientCard.SetActive(true);
    }

    public void HideHud() {
        transparencyPanel.SetActive(false);
        UIDishCard.SetActive(false);
        ingredientCard.SetActive(false);        
        panelSpaceBar.SetActive(false);
    }

    public void DisplayGrillTimer() {
        if (controller.GetPattyStatus(currentIndex) == 0) {
            grillTimer.SetActive(false);
            return;
        }
        grillTimer.SetActive(true);
        if (controller.GetPattyStatus(currentIndex) == 3) {
            cookedPattySlider.value = 1;
            burntPattySlider.value = 1;
        }
        else if (controller.GetPattyStatus(currentIndex) == 2) {
            cookedPattySlider.value = 1;
            burntPattySlider.value = (5 - controller.GetPattyTimer(currentIndex)) / 5.0f;
        } else {
            cookedPattySlider.value = (5 - controller.GetPattyTimer(currentIndex)) / 5.0f;
            burntPattySlider.value = 0;
        }
    }

    public void DisplayOvenTimer() {
        if (controller.GetPizzaStatus(currentIndex) == 0) {
            ovenTimer.SetActive(false);
            return;
        }
        ovenTimer.SetActive(true);
        if (controller.GetPizzaStatus(currentIndex) == 3) {
            cookedPizzaSlider.value = 1;
            burntPizzaSlider.value = 1;
        } else if (controller.GetPizzaStatus(currentIndex) == 2) {
            cookedPizzaSlider.value = 1;
            burntPizzaSlider.value = (5 - controller.GetPizzaTimer(currentIndex)) / 5.0f;
        } else {
            cookedPizzaSlider.value = (5 - controller.GetPizzaTimer(currentIndex)) / 5.0f;
            burntPizzaSlider.value = 0;
        }
    }

    public void DisplayDressingProgressBar() {
        dressingProgessBar.SetActive(true);
        if (dressingOrdered[currentIndex] <= 2) {
            dressingSlider.value = controller.GetRanchAmount(currentIndex) / 3.5f;
        } else if (dressingOrdered[currentIndex] <= 5) {
            dressingSlider.value = controller.GetThousandAmount(currentIndex) / 3.5f;
        } else if (dressingOrdered[currentIndex] <= 8) {
            dressingSlider.value = controller.GetVinaigretteAmount(currentIndex) / 3.5f;
        }
    }

    public void HideGrillTimer() {
        cookedPattySlider.value = 0;
        burntPattySlider.value = 0;
        grillTimer.SetActive(false);
    }

    public void HideStations() {
        controller.DestroyCurrentPatty();
        controller.DestroyCurrentPizza();
        controller.DestroyCurrentDressing();
        grillTimer.SetActive(false);
        ovenTimer.SetActive(false);
        dressingProgessBar.SetActive(false);
        panelGrill.SetActive(false);
        panelOven.SetActive(false);
        panelSpaceBar.SetActive(false);
    }

    public void HideOvenTimer() {
        cookedPizzaSlider.value = 0;
        burntPizzaSlider.value = 0;
        ovenTimer.SetActive(false);
    }

    public void HideDressingProgressBar() {
        dressingSlider.value = 0;
        dressingProgessBar.SetActive(false);
    }

    public void DisplaySpaceBar(string message) {
        panelSpaceBarText.text = message;
        panelSpaceBar.SetActive(true);
    }

    public void ResetCookingUI(int index) {
        for (int i = 0; i < cookingSlots.Length; i++) {
            highlightedKeys[currentIndex, i] = false;
        }
        currentIngredient[index] = 0;
        ResetGrill(index);
        ResetOven(index);
        ResetDressing(index);
    }

    public void SetDressingOrdered(int index) {
        Debug.Log("Setting dressing ordered for " + index.ToString());
        dressingOrdered[index] = (int) UnityEngine.Random.Range(0, 9);
    }

    public void SetDressingDescription() {
        if (dressingOrdered[currentIndex] == 0) {
            controller.SetDescription("Little bit of Ranch");
        } else if (dressingOrdered[currentIndex] == 1) {
            controller.SetDescription("Moderate amount of Ranch");
        } else if (dressingOrdered[currentIndex] == 2) {
            controller.SetDescription("Lot of Ranch");
        }else if (dressingOrdered[currentIndex] == 3) {
            controller.SetDescription("Little bit of Thousand Island sauce");
        }else if (dressingOrdered[currentIndex] == 4) {
            controller.SetDescription("Moderate amount of Thousand Island sauce");
        }else if (dressingOrdered[currentIndex] == 5) {
            controller.SetDescription("Lot of Thousand Island sauce");
        }else if (dressingOrdered[currentIndex] == 6) {
            controller.SetDescription("Little bit of Vinaigrette");
        }else if (dressingOrdered[currentIndex] == 7) {
            controller.SetDescription("Moderate amount of Vinaigrette");
        }else if (dressingOrdered[currentIndex] == 8) {
            controller.SetDescription("Lot of Vinaigrette");
        }
    }
    public string GetDressingOrdered () {
        if (dressingOrdered[currentIndex] <= 2) return "Ranch";
        else if (dressingOrdered[currentIndex] <= 5) return "Thousand";
        else return "Vinaigrette";
    }

    public bool SauceMatchesOrderedDressing(string sauce) {
        return sauce == GetDressingOrdered();
    }

    public bool SaladReadyToServe() {
        return controller.GetRanchStage(currentIndex) == (dressingOrdered[currentIndex] % 3);
    }
}
