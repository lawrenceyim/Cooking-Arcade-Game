using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CookingUI : MonoBehaviour
{
    [SerializeField] GameObject[] cookingSlots;
    [SerializeField] TextMeshPro[] ingredientNames;
    [SerializeField] TextMeshPro[] buttonKeys;
    [SerializeField] GameObject serveButton;
    [SerializeField] GameObject restartButton;
    Recipe.FoodTypes currentFoodtype;
    Dictionary<KeyCode, Recipe.Ingredients> availableKeys;

    void Start()
    {
        availableKeys = new Dictionary<KeyCode, Recipe.Ingredients>();
        ingredientNames = new TextMeshPro[cookingSlots.Length];
        buttonKeys = new TextMeshPro[cookingSlots.Length];
        for (int i = 0; i < cookingSlots.Length; i++) {
            ingredientNames[i] = cookingSlots[i].transform.Find("Text").GetComponent<TextMeshPro>();
            buttonKeys[i] = cookingSlots[i].transform.Find("Button").transform.Find("Text").GetComponent<TextMeshPro>();
        }
        DeactivateButtons();
    }

    void Update() {
        ProcessInput();
    }

    public void UpdateButtons(string name) {
        DeactivateButtons();
        currentFoodtype = Recipe.foodTypes[name];
        availableKeys = Recipe.GetCurrentKeys(currentFoodtype);
        List<Recipe.Ingredients> ingredients = Recipe.GetAllIngredients(name);
        for (int i = 0; i < ingredients.Count; i++) {
            cookingSlots[i].SetActive(true);
            ingredientNames[i].text = ingredients[i].ToString();
            buttonKeys[i].text = Recipe.keyMapping[ingredients[i]].ToString();
        }
        serveButton.SetActive(true);
        restartButton.SetActive(true);
    }

    public void DeactivateButtons() {
        serveButton.SetActive(false);
        restartButton.SetActive(false);
        for (int i = 0; i < cookingSlots.Length; i++) {
            ingredientNames[i].text = "";
            buttonKeys[i].text = "";
            cookingSlots[i].SetActive(false);
        }
    }

    void ProcessInput() {
        foreach (KeyCode key in availableKeys.Keys) {
            if (Input.GetKeyDown(key)) {
                Debug.Log($"{key} pressed.");
            }
        }
        // if (Input.GetKey(KeyCode.A)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.B)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.C)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.D)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.E)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.F)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.G)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.H)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.I)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.J)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.K)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.L)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.M)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.N)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.O)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.P)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.Q)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.R)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.S)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.T)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.U)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.V)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.W)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.X)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.Y)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
        // if (Input.GetKey(KeyCode.Z)) {	
        //     if (currentFoodtype == Recipe.FoodTypes.Salad) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Burger) {
                
        //     } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {
                
        //     }
        // }
    }

    void SetButtonVisual(int buttonIndex) {
        if (currentFoodtype == Recipe.FoodTypes.Burger) {

        } else if (currentFoodtype == Recipe.FoodTypes.Pizza) {

        } else if (currentFoodtype == Recipe.FoodTypes.Salad) {

        }
    }
}
