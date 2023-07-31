using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    [SerializeField] public string dishName;
    [SerializeField] public Recipe.FoodTypes foodType;
    [SerializeField] public List<Recipe.Ingredients> ingredientsList;
    [SerializeField] public string description;
    [SerializeField] public int sellingPrice;
}
