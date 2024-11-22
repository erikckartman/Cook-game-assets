using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    [SerializeField] private float cookTime; //Time for fried dish
    private Product ingredientOnOven;

    private void OnCollisionEnter(Collision other)  //Starts cooking process if dish still on the stove
    {
        Product ingredient = other.gameObject.GetComponent<Product>();

        if (ingredient != null)
        {
            ingredientOnOven = ingredient;
            StartCoroutine(Cooking());
        }
    }

    private void OnCollisionExit(Collision other)   //Ends cooking process if dish now not on the stove
    {
        Product ingredient = other.gameObject.GetComponent<Product>();

        if (ingredient != null && ingredient == ingredientOnOven)
        {
            ingredientOnOven = null;
        }
    }

    private IEnumerator Cooking()
    {
        //Fried dish
        if (ingredientOnOven != null && ingredientOnOven.state == TempState.Raw)
        {
            ingredientOnOven.gameObject.SetActive(false);
            yield return new WaitForSeconds(cookTime);
            ingredientOnOven.gameObject.SetActive(true);

            ingredientOnOven.ChangeState(TempState.Baked);
        }
    }
}
