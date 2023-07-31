using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Recipe
{
    public enum Ingredients {
        avocado,
        bacon,
        buns,
        carrots,
        cheese,
        croutons,
        cucumbers,
        dough,
        eggs,
        feta,
        lettuce,
        ham,
        mozzarella,
        mushrooms,
        olives,
        onions,
        parmesan,
        patty, 
        pepperoni,
        peppers,
        pineapple,
        sauce,
        sausage,
        tomatoes,
    }

    public enum FoodTypes {
        Burger,
        Pizza, 
        Salad
    }

    public static Dictionary<FoodTypes, List<Ingredients>> ingredientsForFoodType = new Dictionary<FoodTypes, List<Ingredients>>();
    public static Dictionary<Ingredients, KeyCode> keyMapping = new Dictionary<Ingredients, KeyCode>();
    public static Dictionary<Ingredients, int> ingredientCost = new Dictionary<Ingredients, int>();
    public static List<Dish> dishList = new List<Dish>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void InitializeRecipes() {
        ingredientsForFoodType.Add(FoodTypes.Burger, new List<Ingredients>{Ingredients.bacon, Ingredients.buns,  
            Ingredients.cheese, Ingredients.lettuce, Ingredients.onions, Ingredients.patty, Ingredients.tomatoes});
        ingredientsForFoodType.Add(FoodTypes.Pizza, new List<Ingredients>{Ingredients.dough, Ingredients.ham, Ingredients.mozzarella, 
            Ingredients.mushrooms, Ingredients.pepperoni, Ingredients.pineapple, Ingredients.sauce, Ingredients.sausage});
        ingredientsForFoodType.Add(FoodTypes.Salad, new List<Ingredients>{Ingredients.avocado, Ingredients.bacon, Ingredients.carrots, 
            Ingredients.croutons, Ingredients.cucumbers, Ingredients.eggs, Ingredients.feta, Ingredients.lettuce, 
            Ingredients.olives, Ingredients.onions, Ingredients.parmesan, Ingredients.tomatoes});
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeKeys() {
        keyMapping.Add(Ingredients.avocado, KeyCode.A);
        keyMapping.Add(Ingredients.bacon, KeyCode.B);
        keyMapping.Add(Ingredients.buns, KeyCode.D);
        keyMapping.Add(Ingredients.carrots, KeyCode.C);
        keyMapping.Add(Ingredients.cheese, KeyCode.C);
        keyMapping.Add(Ingredients.croutons, KeyCode.D);
        keyMapping.Add(Ingredients.cucumbers, KeyCode.K);
        keyMapping.Add(Ingredients.dough, KeyCode.D);
        keyMapping.Add(Ingredients.eggs, KeyCode.E);
        keyMapping.Add(Ingredients.feta, KeyCode.F);
        keyMapping.Add(Ingredients.lettuce, KeyCode.L);
        keyMapping.Add(Ingredients.ham, KeyCode.H);
        keyMapping.Add(Ingredients.mozzarella, KeyCode.C);
        keyMapping.Add(Ingredients.mushrooms, KeyCode.M);
        keyMapping.Add(Ingredients.olives, KeyCode.O);
        keyMapping.Add(Ingredients.onions, KeyCode.N);
        keyMapping.Add(Ingredients.parmesan, KeyCode.J);
        keyMapping.Add(Ingredients.patty, KeyCode.P);
        keyMapping.Add(Ingredients.pepperoni, KeyCode.P);
        keyMapping.Add(Ingredients.peppers, KeyCode.V);
        keyMapping.Add(Ingredients.pineapple, KeyCode.F);
        keyMapping.Add(Ingredients.sauce, KeyCode.S);
        keyMapping.Add(Ingredients.sausage, KeyCode.R);
        keyMapping.Add(Ingredients.tomatoes, KeyCode.T);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializePrice() {
        //ingredientCost.Add(Ingredients.avocado, );
       ingredientCost.Add(Ingredients.bacon, 3);
       ingredientCost.Add(Ingredients.buns, 1);
        //ingredientCost.Add(Ingredients.carrots, );
       ingredientCost.Add(Ingredients.cheese, 2);
        //ingredientCost.Add(Ingredients.croutons, );
        //ingredientCost.Add(Ingredients.cucumbers, );
        //ingredientCost.Add(Ingredients.dough, );
        //ingredientCost.Add(Ingredients.eggs, );
        //ingredientCost.Add(Ingredients.feta, );
       ingredientCost.Add(Ingredients.lettuce, 1);
        //ingredientCost.Add(Ingredients.ham, );
        //ingredientCost.Add(Ingredients.mozzarella, );
        //ingredientCost.Add(Ingredients.mushrooms, );
        //ingredientCost.Add(Ingredients.olives, );
       ingredientCost.Add(Ingredients.onions, 1);
        //ingredientCost.Add(Ingredients.parmesan, );
       ingredientCost.Add(Ingredients.patty, 3);
        //ingredientCost.Add(Ingredients.pepperoni, );
        //ingredientCost.Add(Ingredients.peppers, );
        //ingredientCost.Add(Ingredients.pineapple, );
        //ingredientCost.Add(Ingredients.sauce, );
        //ingredientCost.Add(Ingredients.sausage, );
       ingredientCost.Add(Ingredients.tomatoes, 1);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void ConvertDictToList() {
        List<GameObject> temp = new List<GameObject>(PrefabCache.instance.dishes);
        foreach (GameObject g in temp) {
            dishList.Add(g.GetComponent<Dish>());
        }
    }

    public static List<Ingredients> GetAllIngredients(Dish dish) {
        return ingredientsForFoodType[dish.foodType];
    }

    public static Dish SelectRandomRecipe() {
        int randomIndex = UnityEngine.Random.Range(0, dishList.Count);
        return dishList[randomIndex];
    }

    public static Dictionary<KeyCode, Ingredients> GetCurrentKeys(FoodTypes foodtype) {
        Dictionary<KeyCode, Ingredients> keys = new Dictionary<KeyCode, Ingredients>();
        List<Ingredients> ingred = ingredientsForFoodType[foodtype];
        for (int i = 0; i < ingred.Count; i++) {
            keys.Add(keyMapping[ingred[i]], ingred[i]);
        }
        return keys;
    }
}
