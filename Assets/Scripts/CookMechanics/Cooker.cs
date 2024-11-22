using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for ingredient
[System.Serializable]
public class IngredientRequirement
{
    public string ingredientName; 
    public int requiredAmount;
    public TempState requiredTempState; 
    public FormState requiredFormState;
}

//Class for recipe
[System.Serializable]
public class Recipe
{
    public string dishName;
    public List<IngredientRequirement> ingredients; 
    public GameObject dishPrefab;
}

public class Cooker : MonoBehaviour
{
    public List<Recipe> recipes;
    private List<Product> currentIngredients = new List<Product>();

    //Check is food on the table
    private void OnCollisionEnter(Collision other)
    {
        Product ingredient = other.gameObject.GetComponent<Product>();
        if (ingredient != null)
        {
            currentIngredients.Add(ingredient);
            CheckForRecipe();
        }
    }

    //Checl is some dish can be made by the ingredients on the table
    private void CheckForRecipe()
    {
        foreach (Recipe recipe in recipes)
        {
            if (IsRecipeFulfilled(recipe))
            {
                Vector3 dishPos = new Vector3(transform.position.x, transform.position.y + (transform.localScale.y / 2), transform.position.z);
                Instantiate(recipe.dishPrefab, dishPos, Quaternion.identity);

                foreach (var ingredient in currentIngredients)
                {
                    Destroy(ingredient.gameObject);
                }
                currentIngredients.Clear();
                break;
            }
        }
    }

    private bool IsRecipeFulfilled(Recipe recipe)
    {
        foreach (IngredientRequirement req in recipe.ingredients)
        {
            int count = 0;

            foreach (Product ingredient in currentIngredients)
            {
                if (ingredient.name == req.ingredientName && ingredient.state == req.requiredTempState && ingredient.stateForm == req.requiredFormState)
                {
                    count++;
                }
            }

            if (count < req.requiredAmount)
                return false;
        }
        return true;
    }
}
