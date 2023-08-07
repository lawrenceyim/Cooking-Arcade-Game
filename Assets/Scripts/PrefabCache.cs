using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabCache : MonoBehaviour
{
    public static PrefabCache instance;
    [SerializeField] public List<GameObject> dishes;
    [SerializeField] public GameObject[] customerPrefab;
    [SerializeField] public GameObject[] ingredientIconPrefab;
    [SerializeField] public GameObject[] dishIconPrefab;
    public Dictionary<Recipe.DishName, GameObject> dishDict;
    public Dictionary<Recipe.Ingredients, Sprite> iconDict;
    public Dictionary<Recipe.DishName, Sprite> dishIconDict;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        dishDict = new Dictionary<Recipe.DishName, GameObject>();
        foreach (GameObject dish in dishes) {
            dishDict.Add(dish.GetComponent<Dish>().dishName, dish);
        }

        iconDict = new Dictionary<Recipe.Ingredients, Sprite>();
        foreach (GameObject icon in ingredientIconPrefab) {
            iconDict.Add(icon.GetComponent<IngredientIcon>().ingredient, icon.GetComponent<SpriteRenderer>().sprite);
        }

        dishIconDict = new Dictionary<Recipe.DishName, Sprite>();
        foreach (GameObject icon in dishIconPrefab) {
            dishIconDict.Add(icon.GetComponent<DishIcon>().dishName, icon.GetComponent<SpriteRenderer>().sprite);
        }

        Recipe.ConvertDictToList();
    }

}
