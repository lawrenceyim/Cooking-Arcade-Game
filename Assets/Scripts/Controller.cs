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

    public float GetTimeLeft() {
        return dataUI.GetTimeLeft();
    }

    public bool OrderSlotIsOccupied(int index) {
        return orderManager.HasOrder(index);
    }

    public void RemoveOrderFromOrderSlot(int index) {
        orderManager.RemoveOrder(index);
    }

    public void SetOrderDescription(Dish dish) {
        description.SetOrderDescription(dish);
    }

    public void UpdateCookingButtons(Dish dish, int index) {
        cookingUI.UpdateButtons(dish, index);
    }

    public void ChangeDishBeingWorkedOn(int index) {
        cooking.ChangeDish(index);        
    }

    public void AddOrderToOrderSlot(int index, Recipe.DishName dishName, float countdown) {
        orderUI.SetOrderSlot(index, dishName, countdown);
    }

    public void AddDishToCookingPanel(int index, GameObject dish) {
        cooking.AddDish(index, dish);
    }

    public void RemoveDishFromCookingPanel(int index) {
        cooking.RemoveDish(index);
    }

    public void ResetOrderSlot(int index) {
        orderUI.ResetOrderSlot(index);
    }

    public void ClearDescriptionPanel() {
        description.ClearOrderDescription();
    }

    public void ClearCookingButtons() {
        cookingUI.DeactivateButtons();
    }

    public void UpdateMoneyUI() {
        dataUI.UpdateMoneyUI();
    }

    public bool IsDishComplete() {
        return cooking.IsDishComplete();
    }

    public void ServeTheDish() {
        orderManager.OrderServed();
    }


    public void ResetDishBeingWorkedOn(int index) {
        cooking.ResetDish(index);
    }

    public bool IngredientAlreadyAdded(Recipe.Ingredients ingredient) {
        return cooking.IngredientAlreadyAdded(ingredient);
    }

    public void AddIngredientToDish(Recipe.Ingredients ingredients) {
        cooking.MakeIngredientVisible(ingredients);
    }

    public bool CheckIfCustomerCountIsZero() {
        return customerSpawner.NoCustomersLeft();
    }
}
