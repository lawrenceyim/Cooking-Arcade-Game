using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CookingUI : MonoBehaviour {
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
    int currentIndex = -1;
    IActivity[] activities;
    Dressing dressing = new Dressing();
    Grill grill = new Grill();
    Oven oven = new Oven();

    void Start() {
        if (controller == null) {
            Debug.LogError("controller script is null");
        }
        activities = new IActivity[6];
        buttonKeys = new TextMeshProUGUI[cookingSlots.Length];
        ingredientImages = new Image[cookingSlots.Length];
        buttonBackgroundHighlights = new Image[cookingSlots.Length];
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
        if (currentIndex < 0 || activities[currentIndex] == null) {
            return;
        }
        foreach (IActivity activity in activities) {
            if (activity == null) {
                continue;
            }
            activity.UpdateActivity(Time.deltaTime);
        }
        activities[currentIndex].ProcessInput();
    }

    public int GetCookingSlotsLength() {
        return cookingSlots.Length;
    }

    public void AddActivity(Dish dish, int index) {
        if (dish.foodType.Equals(Recipe.FoodTypes.Burger)) {
            activities[index] = new BurgerActivity(this, controller, dish, index, grill);
        } else if (dish.foodType.Equals(Recipe.FoodTypes.Pizza)) {
            activities[index] = new PizzaActivity(this, controller, dish, index, oven);
        } else if (dish.foodType.Equals(Recipe.FoodTypes.Salad)) {
            activities[index] = new SaladActivity(this, controller, dish, index, dressing);
        }
    }

    public void UpdateButtons(int index) {
        DisplayHud();
        HideStations();
        DeactivateButtons();
        AudioManager.instance.StopPlayingSound();
         if (currentIndex >= 0 && activities[currentIndex] != null) {
            activities[currentIndex].ClearDisplay();
        }
        currentIndex = index;
        activities[index].SetupDisplay();
    }

    public void DeactivateButtons() {
        for (int i = 0; i < cookingSlots.Length; i++) {
            ingredientImages[i].enabled = false;
            UnhighlightButtonBackground(i);
            SetButtonText("", i);
        }
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
        grillTimer.SetActive(true);
    }

    public void DisplayOvenTimer() {
        ovenTimer.SetActive(true);
    }

    public void HideGrillTimer() {
        cookedPattySlider.value = 0;
        burntPattySlider.value = 0;
        grillTimer.SetActive(false);
    }

    public void HideStations() {
        HideGrill();
        HideOven();
        HideGrillTimer();
        HideOvenTimer();
        HideDressingProgressBar();
    }

    public void HidePanelSpaceBar() {
        panelSpaceBar.SetActive(false);
    }

    public void ShowPanelSpaceBar() {
        panelSpaceBar.SetActive(true);
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

    public void ResetButtonHighlights() {
        for (int i = 0; i < cookingSlots.Length; i++) {
            UnhighlightButtonBackground(i);
        }
    }

    public void HideGrill() {
        panelGrill.SetActive(false);
    }

    public void HideOven() {
        panelOven.SetActive(false);
    }

    public void HideSalad() {
        dressingProgessBar.SetActive(false);
    }

    public void HighlightButtonBackground(int keyIndex) {
        buttonBackgroundHighlights[keyIndex].enabled = true;
    }

    public void UnhighlightButtonBackground(int keyIndex) {
        buttonBackgroundHighlights[keyIndex].enabled = false;
    }

    public void SetCookedPattySlider(float value) {
        cookedPattySlider.value = value;
    }

    public void SetBurntPattySlider(float value) {
        burntPattySlider.value = value;
    }

    public void SetCookedPizzaSlider(float value) {
        cookedPizzaSlider.value = value;
    }

    public void SetBurntPizzaSlider(float value) {
        burntPizzaSlider.value = value;
    }

    public void SetDressingProgressBar(float value) {
        dressingProgessBar.SetActive(true);
        dressingSlider.value = value;
    }

    public void SetIngredientImage(Sprite sprite, int index) {
        ingredientImages[index].sprite = sprite;
        ingredientImages[index].enabled = true;
    }

    public void SetButtonText(String text, int index) {
        buttonKeys[index].text = text;
    }

    public void ShowIngredientCard() {
        ingredientCard.SetActive(true);
    }

    public void HideIngredientCard() {
        ingredientCard.SetActive(false);
    }

    public void DisplayGrill() {
        panelGrill.SetActive(true);
    }

    public void DisplayOven() {
        panelOven.SetActive(true);
    }

    public void SetCurrentIndex(int index) {
        currentIndex = index;
    }
}
