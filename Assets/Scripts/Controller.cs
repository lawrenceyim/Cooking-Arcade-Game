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
    [SerializeField] OrderManager orderManager;
    [SerializeField] OrderUI orderUI;

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

    public void UpdateCookingButtons(Dish dish, int index) {
        cookingUI.UpdateButtons(dish, index);
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

    public void SetOrderDescription(Dish dish) {
        description.SetOrderDescription(dish);
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
}
