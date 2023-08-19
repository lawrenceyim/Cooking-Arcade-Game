using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

public static class Recipe
{
    public enum Ingredients {
        avocado,
        bacon_burger,
        bacon_pizza,
        bacon_salad,
        bun_bottom,
        bun_top,
        carrots,
        cheese,
        croutons,
        cucumbers,
        dough,
        eggs,
        feta,
        lettuce_burger,
        lettuce_salad,
        ham,
        mozzarella,
        mushrooms_pizza,
        olives,
        onions_burger,
        onions_salad,
        parmesan,
        patty, 
        pepperoni,
        peppers,
        pineapple,
        sauce,
        sausage,
        tomatoes_burger,
        tomatoes_salad,
    }

    public enum FoodTypes {
        Burger,
        Pizza, 
        Salad
    }

    public enum DishName {
        TheWorks,
        ClassicBurger,
        Cheeseburger,
        BaconCheeseburger,
        CheesePizza,
        PepperoniPizza,
        HawaiianPizza,
        VegetarianPizza,
    }

    public static Dictionary<FoodTypes, List<Ingredients>> ingredientsForFoodType = new Dictionary<FoodTypes, List<Ingredients>>();
    public static Dictionary<Ingredients, KeyCode> keyMapping = new Dictionary<Ingredients, KeyCode>();
    public static Dictionary<Ingredients, int> ingredientCost = new Dictionary<Ingredients, int>();
    public static Dictionary<DishName, string> dishNameString = new Dictionary<DishName, string>();
    public static List<Dish> dishList = new List<Dish>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void InitializeRecipes() {
        ingredientsForFoodType.Add(FoodTypes.Burger, new List<Ingredients>{Ingredients.bacon_burger, Ingredients.bun_bottom, Ingredients.bun_top,  
            Ingredients.cheese, Ingredients.lettuce_burger, Ingredients.onions_burger, Ingredients.patty, Ingredients.tomatoes_burger});
        ingredientsForFoodType.Add(FoodTypes.Pizza, new List<Ingredients>{Ingredients.bacon_pizza, Ingredients.dough, Ingredients.ham, Ingredients.mozzarella, 
            Ingredients.mushrooms_pizza, Ingredients.peppers, Ingredients.pepperoni, Ingredients.pineapple, Ingredients.sauce});
        ingredientsForFoodType.Add(FoodTypes.Salad, new List<Ingredients>{Ingredients.avocado, Ingredients.bacon_pizza, Ingredients.carrots, 
            Ingredients.croutons, Ingredients.cucumbers, Ingredients.eggs, Ingredients.feta, Ingredients.lettuce_salad, 
            Ingredients.olives, Ingredients.onions_salad, Ingredients.parmesan, Ingredients.tomatoes_salad});
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeKeys() {
        keyMapping.Add(Ingredients.avocado, KeyCode.A);
        keyMapping.Add(Ingredients.bacon_burger, KeyCode.B);
        keyMapping.Add(Ingredients.bacon_pizza, KeyCode.B);
        keyMapping.Add(Ingredients.bun_bottom, KeyCode.S);
        keyMapping.Add(Ingredients.bun_top, KeyCode.W);
        keyMapping.Add(Ingredients.carrots, KeyCode.C);
        keyMapping.Add(Ingredients.cheese, KeyCode.C);
        keyMapping.Add(Ingredients.croutons, KeyCode.D);
        keyMapping.Add(Ingredients.cucumbers, KeyCode.K);
        keyMapping.Add(Ingredients.dough, KeyCode.D);
        keyMapping.Add(Ingredients.eggs, KeyCode.E);
        keyMapping.Add(Ingredients.feta, KeyCode.F);
        keyMapping.Add(Ingredients.lettuce_burger, KeyCode.L);
        keyMapping.Add(Ingredients.ham, KeyCode.H);
        keyMapping.Add(Ingredients.mozzarella, KeyCode.C);
        keyMapping.Add(Ingredients.mushrooms_pizza, KeyCode.M);
        keyMapping.Add(Ingredients.olives, KeyCode.N);
        keyMapping.Add(Ingredients.onions_burger, KeyCode.O);
        keyMapping.Add(Ingredients.parmesan, KeyCode.J);
        keyMapping.Add(Ingredients.patty, KeyCode.P);
        keyMapping.Add(Ingredients.pepperoni, KeyCode.P);
        keyMapping.Add(Ingredients.peppers, KeyCode.V);
        keyMapping.Add(Ingredients.pineapple, KeyCode.F);
        keyMapping.Add(Ingredients.sauce, KeyCode.S);
        keyMapping.Add(Ingredients.sausage, KeyCode.R);
        keyMapping.Add(Ingredients.tomatoes_burger, KeyCode.T);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializePrice() {
        //ingredientCost.Add(Ingredients.avocado, );
       ingredientCost.Add(Ingredients.bacon_burger, 3);
       ingredientCost.Add(Ingredients.bacon_pizza, 3);
       ingredientCost.Add(Ingredients.bun_bottom, 1);
       ingredientCost.Add(Ingredients.bun_top, 1);
        //ingredientCost.Add(Ingredients.carrots, );
       ingredientCost.Add(Ingredients.cheese, 2);
        //ingredientCost.Add(Ingredients.croutons, );
        //ingredientCost.Add(Ingredients.cucumbers, );
        ingredientCost.Add(Ingredients.dough, 1);
        //ingredientCost.Add(Ingredients.eggs, );
        //ingredientCost.Add(Ingredients.feta, );
        ingredientCost.Add(Ingredients.lettuce_burger, 1);
        ingredientCost.Add(Ingredients.ham, 3);
        ingredientCost.Add(Ingredients.mozzarella, 2);
        ingredientCost.Add(Ingredients.mushrooms_pizza, 2);
        //ingredientCost.Add(Ingredients.olives, );
        ingredientCost.Add(Ingredients.onions_burger, 1);
        //ingredientCost.Add(Ingredients.parmesan, );
        ingredientCost.Add(Ingredients.patty, 3);
        ingredientCost.Add(Ingredients.pepperoni, 2);
        ingredientCost.Add(Ingredients.peppers, 2);
        ingredientCost.Add(Ingredients.pineapple, 2);
        ingredientCost.Add(Ingredients.sauce, 1);
        //ingredientCost.Add(Ingredients.sausage, );
        ingredientCost.Add(Ingredients.tomatoes_burger, 1);
    }

    // public static void ConvertDictToList() {
    //     List<GameObject> temp = new List<GameObject>(PrefabCache.instance.dishes);
    //     foreach (GameObject g in temp) {
    //         dishList.Add(g.GetComponent<Dish>());
    //     }
    // }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeDishNames() {
        dishNameString.Add(DishName.BaconCheeseburger, "Bacon CheeseBurger");
        dishNameString.Add(DishName.ClassicBurger, "Classic Burger");
        dishNameString.Add(DishName.TheWorks, "The Works");
        dishNameString.Add(DishName.Cheeseburger, "Cheese Burger");
        dishNameString.Add(DishName.CheesePizza, "Cheese Pizza");
        dishNameString.Add(DishName.PepperoniPizza, "Pepperoni Pizza");
        dishNameString.Add(DishName.HawaiianPizza, "Hawaiian Pizza");
        dishNameString.Add(DishName.VegetarianPizza, "Vegetarian Pizza");

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

    public static void SetDistList(int day) {
        dishList = GetNewDishList(day);
    }              

    public static List<Dish> GetNewDishList(int day) {
        return day switch {
            1 => new List<Dish>()   {PrefabCache.instance.dishByDishName[DishName.ClassicBurger]},
            2 => new List<Dish>()   {PrefabCache.instance.dishByDishName[DishName.ClassicBurger],
                                    PrefabCache.instance.dishByDishName[DishName.Cheeseburger]},
            3 => new List<Dish>()   {PrefabCache.instance.dishByDishName[DishName.ClassicBurger],
                                    PrefabCache.instance.dishByDishName[DishName.Cheeseburger],
                                    PrefabCache.instance.dishByDishName[DishName.BaconCheeseburger]},
            4 => new List<Dish>()   {PrefabCache.instance.dishByDishName[DishName.ClassicBurger],
                                    PrefabCache.instance.dishByDishName[DishName.Cheeseburger],
                                    PrefabCache.instance.dishByDishName[DishName.BaconCheeseburger]},
            5 => new List<Dish>()   {PrefabCache.instance.dishByDishName[DishName.ClassicBurger],
                                    PrefabCache.instance.dishByDishName[DishName.Cheeseburger],
                                    PrefabCache.instance.dishByDishName[DishName.BaconCheeseburger],
                                    PrefabCache.instance.dishByDishName[DishName.TheWorks]},
            6 => new List<Dish>()   {PrefabCache.instance.dishByDishName[DishName.ClassicBurger],
                                    PrefabCache.instance.dishByDishName[DishName.Cheeseburger],
                                    PrefabCache.instance.dishByDishName[DishName.BaconCheeseburger],
                                    PrefabCache.instance.dishByDishName[DishName.TheWorks]},
            7 => new List<Dish>()   {PrefabCache.instance.dishByDishName[DishName.ClassicBurger],
                                    PrefabCache.instance.dishByDishName[DishName.Cheeseburger],
                                    PrefabCache.instance.dishByDishName[DishName.BaconCheeseburger],
                                    PrefabCache.instance.dishByDishName[DishName.TheWorks]},
            8 => new List<Dish>()   {PrefabCache.instance.dishByDishName[DishName.CheesePizza]},
            9 => new List<Dish>()   {PrefabCache.instance.dishByDishName[DishName.CheesePizza],
                                    PrefabCache.instance.dishByDishName[DishName.PepperoniPizza]},
            10 => new List<Dish>()  {PrefabCache.instance.dishByDishName[DishName.CheesePizza],
                                    PrefabCache.instance.dishByDishName[DishName.PepperoniPizza],
                                    PrefabCache.instance.dishByDishName[DishName.VegetarianPizza]},
            11 => new List<Dish>()  {PrefabCache.instance.dishByDishName[DishName.CheesePizza],
                                    PrefabCache.instance.dishByDishName[DishName.PepperoniPizza],
                                    PrefabCache.instance.dishByDishName[DishName.VegetarianPizza],
                                    PrefabCache.instance.dishByDishName[DishName.HawaiianPizza]},                    



            _ => null
        };
    }
}
