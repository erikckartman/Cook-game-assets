using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desc : MonoBehaviour
{
    private float cutTime = 3.0f;
    private Product ingredientOnBoard;

    private void OnCollisionEnter(Collision other)
    {
        Product ingredient = other.gameObject.GetComponent<Product>();

        if (ingredient != null)
        {
            ingredientOnBoard = ingredient;
            StartCoroutine(Cooking());
        }
    }

    private void OnCollisionExit(Collision other)
    {
        Product ingredient = other.gameObject.GetComponent<Product>();

        if (ingredient != null && ingredient == ingredientOnBoard)
        {
            ingredientOnBoard = null;
        }
    }

    private IEnumerator Cooking()
    {
        if (ingredientOnBoard != null && ingredientOnBoard.stateForm == FormState.Original)
        {
            yield return new WaitForSeconds(cutTime);
            ingredientOnBoard.CutDish(FormState.Chopped);
        }
    }
}
