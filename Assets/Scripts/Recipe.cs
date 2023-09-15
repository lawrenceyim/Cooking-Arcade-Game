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
        chicken,
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
        CaesarSalad,
        GardenSalad,
        ChickenSalad,
        GreekSalad,
        // MeatLoversPizza
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
        ingredientsForFoodType.Add(FoodTypes.Salad, new List<Ingredients>{Ingredients.bacon_salad, Ingredients.carrots, Ingredients.chicken,
            Ingredients.croutons, Ingredients.cucumbers, Ingredients.lettuce_salad, Ingredients.olives, Ingredients.onions_salad, Ingredients.tomatoes_salad});
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeKeys() {
        keyMapping.Add(Ingredients.avocado, KeyCode.A);
        keyMapping.Add(Ingredients.bacon_burger, KeyCode.B);
        keyMapping.Add(Ingredients.bacon_pizza, KeyCode.B);
        keyMapping.Add(Ingredients.bacon_salad, KeyCode.B);
        keyMapping.Add(Ingredients.bun_bottom, KeyCode.S);
        keyMapping.Add(Ingredients.bun_top, KeyCode.W);
        keyMapping.Add(Ingredients.carrots, KeyCode.V);
        keyMapping.Add(Ingredients.cheese, KeyCode.C);
        keyMapping.Add(Ingredients.chicken, KeyCode.C);
        keyMapping.Add(Ingredients.croutons, KeyCode.D);
        keyMapping.Add(Ingredients.cucumbers, KeyCode.K);
        keyMapping.Add(Ingredients.dough, KeyCode.D);
        keyMapping.Add(Ingredients.eggs, KeyCode.E);
        keyMapping.Add(Ingredients.feta, KeyCode.F);
        keyMapping.Add(Ingredients.lettuce_burger, KeyCode.L);
        keyMapping.Add(Ingredients.lettuce_salad, KeyCode.L);
        keyMapping.Add(Ingredients.ham, KeyCode.H);
        keyMapping.Add(Ingredients.mozzarella, KeyCode.C);
        keyMapping.Add(Ingredients.mushrooms_pizza, KeyCode.M);
        keyMapping.Add(Ingredients.olives, KeyCode.O);
        keyMapping.Add(Ingredients.onions_burger, KeyCode.O);
        keyMapping.Add(Ingredients.onions_salad, KeyCode.N);
        keyMapping.Add(Ingredients.parmesan, KeyCode.J);
        keyMapping.Add(Ingredients.patty, KeyCode.P);
        keyMapping.Add(Ingredients.pepperoni, KeyCode.P);
        keyMapping.Add(Ingredients.peppers, KeyCode.V);
        keyMapping.Add(Ingredients.pineapple, KeyCode.F);
        keyMapping.Add(Ingredients.sauce, KeyCode.S);
        keyMapping.Add(Ingredients.sausage, KeyCode.R);
        keyMapping.Add(Ingredients.tomatoes_burger, KeyCode.T);
        keyMapping.Add(Ingredients.tomatoes_salad, KeyCode.T);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializePrice() {
        ingredientCost.Add(Ingredients.bacon_burger, 3);
        ingredientCost.Add(Ingredients.bacon_salad, 3);
        ingredientCost.Add(Ingredients.bun_bottom, 1);
        ingredientCost.Add(Ingredients.bun_top, 1);
        ingredientCost.Add(Ingredients.carrots, 1);
        ingredientCost.Add(Ingredients.cheese, 2);
        ingredientCost.Add(Ingredients.chicken, 3);
        ingredientCost.Add(Ingredients.croutons, 1);
        ingredientCost.Add(Ingredients.cucumbers, 1);
        ingredientCost.Add(Ingredients.dough, 1);
        ingredientCost.Add(Ingredients.lettuce_burger, 1);
        ingredientCost.Add(Ingredients.lettuce_salad, 1);
        ingredientCost.Add(Ingredients.ham, 3);
        ingredientCost.Add(Ingredients.mozzarella, 2);
        ingredientCost.Add(Ingredients.mushrooms_pizza, 2);
        ingredientCost.Add(Ingredients.olives, 1);
        ingredientCost.Add(Ingredients.onions_burger, 1);
        ingredientCost.Add(Ingredients.onions_salad, 1);
        ingredientCost.Add(Ingredients.patty, 3);
        ingredientCost.Add(Ingredients.pepperoni, 2);
        ingredientCost.Add(Ingredients.peppers, 2);
        ingredientCost.Add(Ingredients.pineapple, 2);
        ingredientCost.Add(Ingredients.sauce, 1);
        ingredientCost.Add(Ingredients.tomatoes_burger, 1);
        ingredientCost.Add(Ingredients.tomatoes_salad, 1);
    }

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
        dishNameString.Add(DishName.CaesarSalad, "Caesar Salad");
        dishNameString.Add(DishName.GardenSalad, "Garden Salad");
        dishNameString.Add(DishName.ChickenSalad, "Chicken Salad");
        dishNameString.Add(DishName.GreekSalad, "Greek Salad");
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

            12 => new List<Dish>()  {PrefabCache.instance.dishByDishName[DishName.CheesePizza],
                                    PrefabCache.instance.dishByDishName[DishName.PepperoniPizza],
                                    PrefabCache.instance.dishByDishName[DishName.VegetarianPizza],
                                    PrefabCache.instance.dishByDishName[DishName.HawaiianPizza],
                                    // PrefabCache.instance.dishByDishName[DishName.MeatLoversPizza]
                                    },       

            13 => new List<Dish>()  {PrefabCache.instance.dishByDishName[DishName.CheesePizza],
                                    PrefabCache.instance.dishByDishName[DishName.PepperoniPizza],
                                    PrefabCache.instance.dishByDishName[DishName.VegetarianPizza],
                                    PrefabCache.instance.dishByDishName[DishName.HawaiianPizza],
                                    // PrefabCache.instance.dishByDishName[DishName.MeatLoversPizza],
                                    PrefabCache.instance.dishByDishName[DishName.ClassicBurger],
                                    PrefabCache.instance.dishByDishName[DishName.Cheeseburger],
                                    PrefabCache.instance.dishByDishName[DishName.BaconCheeseburger],
                                    PrefabCache.instance.dishByDishName[DishName.TheWorks]},         

            14 => new List<Dish>()  {PrefabCache.instance.dishByDishName[DishName.CheesePizza],
                                    PrefabCache.instance.dishByDishName[DishName.PepperoniPizza],
                                    PrefabCache.instance.dishByDishName[DishName.VegetarianPizza],
                                    PrefabCache.instance.dishByDishName[DishName.HawaiianPizza],
                                    // PrefabCache.instance.dishByDishName[DishName.MeatLoversPizza],
                                    PrefabCache.instance.dishByDishName[DishName.ClassicBurger],
                                    PrefabCache.instance.dishByDishName[DishName.Cheeseburger],
                                    PrefabCache.instance.dishByDishName[DishName.BaconCheeseburger],
                                    PrefabCache.instance.dishByDishName[DishName.TheWorks]},     
            15 => new List<Dish>() {PrefabCache.instance.dishByDishName[DishName.CaesarSalad]},
            
            16 => new List<Dish>() {PrefabCache.instance.dishByDishName[DishName.CaesarSalad],
                                    PrefabCache.instance.dishByDishName[DishName.GardenSalad]},

            17 => new List<Dish>() {PrefabCache.instance.dishByDishName[DishName.CaesarSalad],
                                    PrefabCache.instance.dishByDishName[DishName.GardenSalad],
                                    PrefabCache.instance.dishByDishName[DishName.ChickenSalad]},
 
            18 => new List<Dish>() {PrefabCache.instance.dishByDishName[DishName.CaesarSalad],
                                    PrefabCache.instance.dishByDishName[DishName.GardenSalad],
                                    PrefabCache.instance.dishByDishName[DishName.ChickenSalad],
                                    PrefabCache.instance.dishByDishName[DishName.GreekSalad]},
            _ => null
        };
    }
}
