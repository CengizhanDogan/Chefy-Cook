using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : Singleton<IngredientManager>
{
    [SerializeField] private Transform originTransform;
    [SerializeField] private Vector3 offset;
    [SerializeField] private int collectableCount;

    [SerializeField] private IngredientPositions ingredientPositions;
    private List<Ingredient> ingredients = new List<Ingredient>();

    private int highestValue;

    void Start()
    {
        AddCollector();
        ingredientPositions = new IngredientPositions(originTransform, offset, collectableCount);
    }

    private void AddCollector()
    {
        if (!TryGetComponent(out Collector c))
            gameObject.AddComponent<Collector>();
    }

    public void CollectIngredient(Ingredient ingredient)
    {
        if (ingredients.Count == ingredientPositions.Count) return;

        if (ingredient.IngredientValue > highestValue) highestValue = ingredient.IngredientValue;

        ingredient.collected = true;

        Transform ingredientTransform = ingredientPositions[ingredients.Count].transform;

        ingredient.transform.position = ingredientTransform.position;
        ingredient.transform.SetParent(ingredientTransform);

        ingredient.transform.localEulerAngles = Vector3.zero;

        ingredients.Add(ingredient);
    }
}