using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//This script has to be added to every product, which is used in cooking
public enum TempState { Raw, Fried, Overfried, Baked } //Raw and fried states of product model
public enum FormState { Original, Chopped } //Cut and uncut states of product model
public enum DishType { Meat, Vegetable, Bread }

public class Product : MonoBehaviour
{
    public string foodname;
    public TempState state = TempState.Raw;
    public FormState stateForm = FormState.Original;
    public DishType currentType;

    //Here you need to add meshes for your model
    public Mesh originalModel; //mesh for uncut dish
    public Mesh choppedModel; //mesh for cut dish

    //Here you need to add in inspector materials for you dish's model
    public Material cookedMaterial; //material of fried dish
    public Material bakedMaterial; //material of baked dish
    public Material overfriedMaterial; //material of overfried dish
    public Material originalMaterial; //material of raw dish

    private Renderer ingredientRenderer; //Component, which used for material change
    private MeshFilter ingredientMesh; //Component, which used for mesh change

    private void Start()
    {
        gameObject.tag = "Food"; //Your project must contain a 'Food' tag
        ingredientRenderer = GetComponent<Renderer>();
        ingredientMesh = GetComponent<MeshFilter>();

        if (ingredientRenderer != null)
        {
            originalMaterial = ingredientRenderer.material;
        }

        UpdateModel();
    }

    public void ChangeState(TempState newState)
    {
        state = newState;
        UpdateModel();
    }

    public void CutDish(FormState newState)
    {
        stateForm = newState;
        UpdateModel();
    }

    private void UpdateModel()
    {
        switch (stateForm)
        {
            case FormState.Original:
                ingredientMesh.mesh = originalModel;
                break;
            case FormState.Chopped:
                ingredientMesh.mesh = choppedModel;
                break;
        }

        if(ingredientRenderer != null)
        {
            switch(state)
            {
                case TempState.Raw:
                    ingredientRenderer.material = originalMaterial;
                    break;
                case TempState.Fried:
                    ingredientRenderer.material = cookedMaterial;
                    break;
                case TempState.Overfried:
                    ingredientRenderer.material = overfriedMaterial;
                    break;
                case TempState.Baked:
                    ingredientRenderer.material = bakedMaterial;
                    break;
            }
        }
    }
}