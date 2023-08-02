using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : MonoBehaviour
{
    OrderManager orderManager;
    GameObject[] dishes = new GameObject[6];
    int[] ingredientsAdded = new int[6];
    int[] ingredientsNeeded = new int[6];
    int currentIndex = 0;

    private void Start() {
        orderManager = GameObject.Find("LevelManager").GetComponent<OrderManager>();
    }

    public void ResetDish(int index) {
        for (int i = 0; i < dishes[index].transform.childCount; i++) {
            dishes[index].transform.GetChild(i).gameObject.SetActive(false);
            ingredientsAdded[index] = 0;
        }
    }

    public void RemoveDish(int index) {
        Destroy(dishes[index]);
        dishes[index] = null;
        ingredientsAdded[index] = 0;
        ingredientsNeeded[index] = 0;
    }

    public void AddDish(int index, GameObject dish) {
        dishes[index] = dish;
        ResetDish(index);
        dishes[index].SetActive(false);
        ingredientsAdded[index] = 0;
        List<Recipe.Ingredients> temp = dish.GetComponent<Dish>().ingredientsList;
        ingredientsNeeded[index] = temp.Count;
    }

    public bool IsDishComplete() {
        return ingredientsAdded[currentIndex] == ingredientsNeeded[currentIndex];
    }

    public void MakeIngredientVisible(Recipe.Ingredients ingredient) {
        for (int i = 0; i < dishes[currentIndex].transform.childCount; i++) {
            if (dishes[currentIndex].transform.GetChild(i).gameObject.GetComponent<Ingredient>().ingredient == ingredient) {
                dishes[currentIndex].transform.GetChild(i).gameObject.SetActive(true);
                ingredientsAdded[currentIndex]++;
                break;
            }
        }
        if (IsDishComplete()) {
            Debug.Log("Dish completed");
            StartCoroutine(wait());
        }
    }

    public void ChangeDish(int index) {
        if (dishes[currentIndex] != null)
            dishes[currentIndex].SetActive(false);
        currentIndex = index;
        dishes[currentIndex].SetActive(true);
    }

    public bool IngredientAlreadyAdded(Recipe.Ingredients ingredient) {
        for (int i = 0; i < dishes[currentIndex].transform.childCount; i++) {
            GameObject ingred = dishes[currentIndex].transform.GetChild(i).gameObject;
            if (ingred.activeSelf && ingred.GetComponent<Ingredient>().ingredient == ingredient) {
                return true;
            }
        }
        return false;
    } 

    IEnumerator wait() {
        yield return new WaitForSecondsRealtime(.1f);
        RemoveDish(currentIndex);
        orderManager.OrderServed();
    }
}

