using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] Cooking cooking;
    [SerializeField] CookingUI cookingUI;
    [SerializeField] CustomerSpawner customerSpawner;
    [SerializeField] DataUI dataUI;
    [SerializeField] Description description;
    [SerializeField] Dressing dressing;
    [SerializeField] EndOfDayUI endOfDayUI;
    [SerializeField] Grill grill;
    [SerializeField] OrderManager orderManager;
    [SerializeField] OrderUI orderUI;
    [SerializeField] Oven oven;

    void Start() {
        if (cooking == null ) {
            Debug.LogError("cooking script is null");
        }
        if (cookingUI == null ) {
            Debug.LogError("cookingUI script is null");
        }
        if (customerSpawner == null ) {
            Debug.LogError("customerSpawner script is null");
        }
        if (dataUI == null ) {
            Debug.LogError("dataUI script is null");
        }
        if (description == null ) {
            Debug.LogError("description script is null");
        }
        if (orderManager == null ) {
            Debug.LogError("orderManager script is null");
        }
        if (orderUI == null ) {
            Debug.LogError("orderUI script is null");
        }
    }

    // Cooking
    public void AddDishToCookingPanel(int index, GameObject dish) {
        cooking.AddDish(index, dish);
    }

    public void AddIngredientToDish(Recipe.Ingredients ingredients) {
        cooking.MakeIngredientVisible(ingredients);
    }

    public void ChangeDishBeingWorkedOn(int index) {
        cooking.ChangeDish(index);        
    }
    
    public bool IngredientAlreadyAdded(Recipe.Ingredients ingredient) {
        return cooking.IngredientAlreadyAdded(ingredient);
    }

    public bool IsDishComplete() {
        return cooking.IsDishComplete();
    }
    
    public void RemoveDishFromCookingPanel(int index) {
        cooking.RemoveDish(index);
    }

    public void ResetDishBeingWorkedOn(int index) {
        cooking.ResetDish(index);
    }

    // CookingUI
    public void ClearCookingButtons() {
        cookingUI.DeactivateButtons();
    }

    public void HideHud() {
        cookingUI.HideHud();
    }

    public void UpdateCookingButtons(Dish dish, int index) {
        cookingUI.UpdateButtons(dish, index);
    }

     public void ResetCookingUI(int index) {
        cookingUI.ResetCookingUI(index);
     }

    public void ResetGrill(int index) {
        cookingUI.ResetGrill(index);
    }

    public void ResetOven(int index) {
        cookingUI.ResetOven(index);
    }

    public void HideStations() {
        cookingUI.HideStations();
    }

    // CustomerSpawner
    public bool CheckIfCustomerCountIsZero() {
        return customerSpawner.NoCustomersLeft();
    }

    // DataUI
    public float GetTimeLeft() {
        return dataUI.GetTimeLeft();
    }

    public void UpdateMoneyUI() {
        dataUI.UpdateMoneyUI();
    }

    // Description
    public void ClearDescriptionPanel() {
        description.ClearOrderDescription();
    }

    public void SetDescription(string desc) {
        description.SetDescription(desc);
    }

    public void SetOrderDescription(Dish dish) {
        description.SetOrderDescription(dish);
    }

    // Dressing
    public void AddRanch(int index, float amount) {
        dressing.AddRanch(index, amount);
    }

    public void AddThousand(int index, float amount) {
        dressing.AddThousand(index, amount);
    }

    public void AddVinaigrette(int index, float amount) {
        dressing.AddVinaigrette(index, amount);
    }

    public void DestroyCurrentDressing() {
        dressing.DestroyCurrentDressing();
    }

    public void DestroyCurrentRanch() {
        dressing.DestroyCurrentRanch();
    }

    public void DestroyCurrentThousand() {
        dressing.DestroyCurrentThousand();
    }

    public void DestroyCurrentVinaigrette() {
        dressing.DestroyCurrentVinaigrette();
    }

    public float GetRanchAmount(int index) {
        return dressing.GetRanchAmount(index);
    }

    public float GetThousandAmount(int index) {
        return dressing.GetThousandAmount(index);
    }

    public float GetVinaigretteAmount(int index) {
        return dressing.GetVinaigretteAmount(index);
    }
    
    public void RemoveDressing(int index) {
        dressing.RemoveDressing(index);
    }

    public void SetCurrentDressingObject(int index, string type) {
        dressing.SetCurrentDressingObject(index, type);
    }

    public bool SauceMatchesOrder(int ranchStatus, int thousandStatus, int vinagriatteStatus) {
        return dressing.SauceMatchesOrder(ranchStatus, thousandStatus, vinagriatteStatus);
    }

    // EndOfDayUI
    public void UpdateSummary() {
        endOfDayUI.UpdateSummaryText();
    }

    // Grill
    public void AddPattyToGrill(int index) {
        grill.AddPattyToGrill(index);
    }
    
    public void DestroyCurrentPatty() {
        grill.DestroyCurrentPatty();
    }

    public int GetPattyStatus(int index) {
        return grill.GetPattyStatus(index);
    }
    
    public float GetPattyTimer(int index) {
        return grill.GetPattyTimer(index);
    }

    public void InstantiateCurrentPatty(GameObject patty) {
        grill.InstantiateCurrentPatty(patty);
    }

    public void RemovePattyFromGrill(int index) {
        grill.RemovePattyFromGrill(index);
    }

    public void SetPattyGameObject(int index) {
        grill.SetPattyGameObject(index);
    }

    public void SetPattyStatus(int index, int status) {
        grill.SetPattyStatus(index, status);
    }

    // OrderManager
    public void AddCustomerOrder(GameObject customer) {
        orderManager.AddOrder(customer);
    }

    public bool OrderSlotIsOccupied(int index) {
        return orderManager.HasOrder(index);
    }

    public void RemoveOrderFromOrderSlot(int index) {
        orderManager.RemoveOrder(index);
    }

    public void ServeTheDish() {
        orderManager.OrderServed();
    }

    // OrderUI
    public void AddOrderToOrderSlot(int index, Recipe.DishName dishName, float countdown) {
        orderUI.SetOrderSlot(index, dishName, countdown);
    }

    public void ResetOrderSlot(int index) {
        orderUI.ResetOrderSlot(index);
    }

    // Oven
    public void AddPizzaToOven(int index) {
        oven.AddPizzaToOven(index);
    }

    public void RemovePizzaFromOven(int index) {
        oven.RemovePizzaFromOven(index);
    }

    public void InstantiateCurrentPizza(GameObject pizza) {
        oven.InstantiateCurrentPizza(pizza);
    }

    public void DestroyCurrentPizza() {
        oven.DestroyCurrentPizza();
    }

    public int GetPizzaStatus(int index) {
        return oven.GetPizzaStatus(index);
    }
    
    public float GetPizzaTimer(int index) {
        return oven.GetPizzaTimer(index);
    }

    public void SetPizzaStatus(int index, int status) {
        oven.SetPizzaStatus(index, status);
    }

    public void SetPizzaGameObject(int index) {
        oven.SetPizzaGameObject(index);
    }
}
