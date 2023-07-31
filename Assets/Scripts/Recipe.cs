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


    public static Dictionary<string, List<Ingredients>> requiredIngredients = new Dictionary<string, List<Ingredients>>();
    public static Dictionary<FoodTypes, List<Ingredients>> ingredientsForFoodType = new Dictionary<FoodTypes, List<Ingredients>>();
    public static Dictionary<string, string> recipeDescription = new Dictionary<string, string>();
    public static Dictionary<string, FoodTypes> foodTypes = new Dictionary<string, FoodTypes>();
    public static Dictionary<Ingredients, KeyCode> keyMapping = new Dictionary<Ingredients, KeyCode>();
    public static List<string> recipeList = new List<string>();
    public static Dictionary<string, GameObject> dishes = new Dictionary<string, GameObject>();
    public static Dictionary<Ingredients, int> ingredientCost = new Dictionary<Ingredients, int>();
    public static Dictionary<string, int> sellingPrice = new Dictionary<string, int>();

    public static void AddRecipe(string name, List<Ingredients> required, FoodTypes type, string description) {
        requiredIngredients.Add(name, required);
        foodTypes.Add(name, type);
        recipeDescription.Add(name, description);
        recipeList.Add(name);

        if (name == "Cheeseburger")
            dishes.Add(name, PrefabCache.instance.CheeseBurger);

            // dishes.Add(name, (GameObject) Resources.Load("Prefab/Food/Dishes/Cheeseburger"));
    
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void InitializeRecipes() {
        ingredientsForFoodType.Add(FoodTypes.Burger, new List<Ingredients>{Ingredients.bacon, Ingredients.buns,  
            Ingredients.cheese, Ingredients.lettuce, Ingredients.onions, Ingredients.patty, Ingredients.tomatoes});
        ingredientsForFoodType.Add(FoodTypes.Pizza, new List<Ingredients>{Ingredients.dough, Ingredients.ham, Ingredients.mozzarella, 
            Ingredients.mushrooms, Ingredients.pepperoni, Ingredients.pineapple, Ingredients.sauce, Ingredients.sausage});
        ingredientsForFoodType.Add(FoodTypes.Salad, new List<Ingredients>{Ingredients.avocado, Ingredients.bacon, Ingredients.carrots, 
            Ingredients.croutons, Ingredients.cucumbers, Ingredients.eggs, Ingredients.feta, Ingredients.lettuce, 
            Ingredients.olives, Ingredients.onions, Ingredients.parmesan, Ingredients.tomatoes});

        AddRecipe("Cheeseburger", new List<Ingredients>{Ingredients.buns, Ingredients.cheese, Ingredients.patty}, 
            FoodTypes.Burger, 
            "Buns, cheese, patty.");
        AddRecipe("The Works", new List<Ingredients>{Ingredients.buns, Ingredients.cheese, Ingredients.lettuce, 
            Ingredients.onions, Ingredients.patty, Ingredients.tomatoes}, 
            FoodTypes.Burger, 
            "Buns, cheese, lettuce, onions, patty, and tomatoes.");
        AddRecipe("Classic Burger", new List<Ingredients>{Ingredients.buns, Ingredients.lettuce, Ingredients.patty, Ingredients.tomatoes}, 
            FoodTypes.Burger, 
            "Buns, lettuce, patty, and tomatoes");
        AddRecipe("Bacon Cheeseburger", new List<Ingredients>{Ingredients.bacon, Ingredients.buns, Ingredients.cheese, 
            Ingredients.patty}, 
            FoodTypes.Burger, 
            "Bacon, buns, cheese, and patty");

        AddRecipe("Classic Pepperoni Pizza", new List<Ingredients>{Ingredients.dough, Ingredients.mozzarella, Ingredients.pepperoni, Ingredients.sauce},
            FoodTypes.Pizza,
            "Dough, mozzarella, pepperoni, and sauce");
        AddRecipe("Meat Lovers Pizza", new List<Ingredients>{Ingredients.dough, Ingredients.ham, Ingredients.mozzarella, 
            Ingredients.pepperoni, Ingredients.sauce, Ingredients.sausage},
            FoodTypes.Pizza,
            "Dough, ham, mozzarella, pepperoni, sauce, and sausage");
        AddRecipe("Hawaiian Pizza", new List<Ingredients>{Ingredients.dough, Ingredients.ham, Ingredients.mozzarella, 
            Ingredients.pineapple, Ingredients.sauce},
            FoodTypes.Pizza,
            "Dough, ham, mozzarella, pineapple, and sauce");
        AddRecipe("Vegetarian Pizza", new List<Ingredients>{Ingredients.dough, Ingredients.mozzarella, Ingredients.mushrooms, 
            Ingredients.peppers, Ingredients.sauce},
            FoodTypes.Pizza,
            "Dough, mozzarella, mushrooms, peppers, and sauce");
        AddRecipe("Cheese Pizza", new List<Ingredients>{Ingredients.dough, Ingredients.mozzarella, Ingredients.sauce},
            FoodTypes.Pizza,
            "Dough, mozzarella, and sauce");

        AddRecipe("Garden Salad", new List<Ingredients>{Ingredients.carrots, Ingredients.cucumbers, Ingredients.lettuce, Ingredients.tomatoes},
            FoodTypes.Salad,
            "Lettuce, tomatoes, cucumbers, and carrots");
        AddRecipe("Caesar Salad", new List<Ingredients>{Ingredients.croutons, Ingredients.lettuce, Ingredients.parmesan},
            FoodTypes.Salad,
            "Lettuce, croutons, and parmesan");
        AddRecipe("Greek Salad", new List<Ingredients>{Ingredients.cucumbers, Ingredients.feta, Ingredients.olives, Ingredients.onions, Ingredients.tomatoes},
            FoodTypes.Salad,
            "Cucumbers, feta, olives, onions, and tomatoes");
        AddRecipe("Cobb Salad", new List<Ingredients>{Ingredients.avocado, Ingredients.bacon, Ingredients.eggs, Ingredients.lettuce, Ingredients.tomatoes},
            FoodTypes.Salad,
            "Avocado, bacon, eggs, lettuce, and tomatoes");
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void InitializeSellingPrices() {
        sellingPrice.Add("Cheeseburger", 10);
        sellingPrice.Add("Classic Burger", 10);
        sellingPrice.Add("The Works", 15);
        sellingPrice.Add("Bacon Cheeseburger", 15);

    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void InitializeKeys() {
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
    public static void InitializePrice() {
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

    public static List<Ingredients> GetRequiredIngredients(string name) {
        return requiredIngredients[name];
    }

    public static List<Ingredients> GetAllIngredients(string name) {
        return ingredientsForFoodType[foodTypes[name]];
    }

    public static string SelectRandomRecipe() {
        int randomIndex = UnityEngine.Random.Range(0, recipeList.Count);
        return recipeList[randomIndex];
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
