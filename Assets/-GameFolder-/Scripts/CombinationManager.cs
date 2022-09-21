using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationManager : MonoBehaviour, IInteractable, IExitable
{
    [SerializeField] private CookingType cookingType;

    private IngredientManager ingredientManager;
    private IngredientMovement ingredientMovement;
    [SerializeField] private float waitCount;
    private WaitForSeconds loop;

    private List<Combination> combinations = new List<Combination>();


    void Start()
    {
        ingredientManager = IngredientManager.Instance;
        loop = new WaitForSeconds(waitCount);

        if (!TryGetComponent(out ingredientMovement)) ingredientMovement = gameObject.AddComponent<IngredientMovement>();
    }
    public void Interact(Interactor interactor)
    {
        List<Ingredient> myIngredients = new List<Ingredient>();
        foreach (var item in ingredientManager.Ingredients) myIngredients.Add(item);

        StartCoroutine(Combinate(myIngredients, ingredientManager.HighestValue));
    }

    public void Exit()
    {
        StopAllCoroutines();
    }

    private IEnumerator Combinate(List<Ingredient> ingredients, int highestValue)
    {
        while (ingredients.Count > 0)
        {
            foreach (var ingredient in ingredients)
            {
                if (ingredient.IngredientValue == highestValue && ingredient.cookingType == cookingType)
                {
                    bool gotIng = false;
                    List<Ingredient> combination = new List<Ingredient>();

                    combination.Add(ingredient);

                    for (int i = highestValue - 1; i >= -1; i--)
                    {
                        gotIng = false;
                        foreach (var secondIng in ingredients)
                        {
                            if (ingredientManager.Ingredients.Contains(secondIng) && secondIng.cookingType == cookingType)
                                if (!gotIng && secondIng.IngredientValue == i)
                                {
                                    combination.Add(secondIng);
                                    RemoveFromList(secondIng);
                                    gotIng = true;
                                }
                        }
                    }
                    if (combination.Count <= 1)
                    {
                        StartCoroutine(SendAll(ingredientManager.Ingredients));
                        yield break;
                    }
                    else
                    {
                        RemoveFromList(ingredient);
                        combinations.Add(new Combination(combination));
                        ingredientMovement.MoveIngredient(new Combination(combination));
                        yield return loop;
                    }
                }

            }

            highestValue--;

            yield return null;
        }
    }

    private IEnumerator SendAll(List<Ingredient> ingredients)
    {
        List<Ingredient> myIngredients = new List<Ingredient>();
        foreach (var item in ingredients) myIngredients.Add(item);

        foreach (var ingredient in myIngredients)
        {
            if (ingredient.cookingType != cookingType) continue;

            ingredientMovement.MoveIngredient(ingredient, false);
            RemoveFromList(ingredient);
            yield return loop;
        }
    }

    private void RemoveFromList(Ingredient ingredient)
    {
        ingredientManager.Ingredients.Remove(ingredient);
        ingredientManager.IngredientPositions[ingredient.MyIndex].full = false;
        ingredient.transform.SetParent(null);
    }
}
[Serializable]
public class Combination
{
    private List<Ingredient> combinedIng = new List<Ingredient>();
    public int Count { get { return combinedIng.Count; } }

    public int CombinationID { get; private set; }

    public void Add(Ingredient ing)
    {
        combinedIng.Add(ing);
    }

    public Combination(List<Ingredient> combination)
    {
        foreach (Ingredient ing in combination)
        {
            Add(ing);
            CombinationID += ing.ID;
        }
    }

    public Ingredient this[int index]
    {
        get { return combinedIng[index]; }
        set { combinedIng[index] = value; }
    }
}
