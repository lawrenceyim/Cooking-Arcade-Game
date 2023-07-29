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

    public static void AddRecipe(string name, List<Ingredients> required, FoodTypes type, string description) {
        requiredIngredients.Add(name, required);
        foodTypes.Add(name, type);
        recipeDescription.Add(name, description);
    }


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

    public static List<string> GetRequiredIngredients(string name) {
        List<Ingredients> enumList = requiredIngredients[name];
        List<string> stringList = new List<string>();
        for (int i = 0; i < enumList.Count; i++) {
            stringList.Add(enumList[i].ToString());
        }
        return stringList;
    }

    public static List<string> GetAllIngredients(string name) {
        List<Ingredients> enumList = ingredientsForFoodType[foodTypes[name]];
        List<string> stringList = new List<string>();
        for (int i = 0; i < enumList.Count; i++) {
            stringList.Add(enumList[i].ToString());
        }
        stringList.Sort();
        return stringList;
    }
}
