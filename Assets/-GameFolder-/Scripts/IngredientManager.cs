using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : Singleton<IngredientManager>
{
    [SerializeField] private Transform originTransform;
    [SerializeField] private Vector3 offset;
    [SerializeField] private int collectableCount;

    public IngredientPositions IngredientPositions { get; private set; }
    private List<Ingredient> ingredients = new List<Ingredient>();
    public List<Ingredient> Ingredients { get { return ingredients; } }

    private int highestValue;
    public int HighestValue { get { return highestValue; } }

    void Start()
    {
        AddCollector();
        IngredientPositions = new IngredientPositions(originTransform, offset, collectableCount);
    }

    private void AddCollector()
    {
        if (!TryGetComponent(out Interactor c))
            gameObject.AddComponent<Interactor>();
    }

    public void CollectIngredient(Ingredient ingredient)
    {
        if (ingredients.Count == IngredientPositions.Count) return;

        if (ingredient.IngredientValue > highestValue) highestValue = ingredient.IngredientValue;

        ingredient.collected = true;

        Transform ingredientTransform = IngredientPositions[ingredients.Count].transform;

        IngredientPositions[ingredients.Count].full = true;

        ingredient.transform.position = ingredientTransform.position;
        ingredient.transform.SetParent(ingredientTransform);

        ingredient.transform.localEulerAngles = Vector3.zero;

        ingredients.Add(ingredient);
    }


}