using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IngredientManager : Singleton<IngredientManager>
{
    private int chefLevel;
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
        if (ingredients.Count == IngredientPositions.Count
            && ingredient.IngredientValue <= chefLevel) return;

        if (ingredient.IngredientValue > highestValue) highestValue = ingredient.IngredientValue;

        ingredient.collected = true;

        Transform ingredientTransform = IngredientPositions[ingredients.Count].transform;

        ingredient.carryTransform = ingredientTransform;
        IngredientPositions[ingredients.Count].full = true;

        StartCoroutine(Collection(ingredient, ingredientTransform));
        ingredient.transform.SetParent(ingredientTransform);

        ingredient.transform.localEulerAngles = Vector3.zero;

        ingredients.Add(ingredient);
    }

    IEnumerator Collection(Ingredient ing, Transform transform)
    {
        while (Vector3.Distance(ing.transform.position, transform.position) > 0.01f)
        {
            ing.transform.position = Vector3.Lerp(ing.transform.position, transform.position, 10f * Time.deltaTime);
            yield return null;
        }
    }

    public void UpgradeCollection()
    {
        IngredientPositions.AddNewPosition();
    }

    public void UpgradeChef()
    {
        chefLevel++;
    }
}