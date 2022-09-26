using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IngredientManager : Singleton<IngredientManager>
{
    [SerializeField] private int chefLevel;
    [SerializeField] private Transform originTransform;
    [SerializeField] private Transform followTransform;
    [SerializeField] private Vector3 offset;
    [SerializeField] private int collectableCount;

    public IngredientPositions IngredientPositions { get; private set; }
    private List<Ingredient> ingredients = new List<Ingredient>();
    public List<Ingredient> Ingredients { get { return ingredients; } }

    private int highestValue;
    public int HighestValue { get { return highestValue; } }

    public int incomeMult;

    void Start()
    {
        AddCollector();
        IngredientPositions = new IngredientPositions(followTransform, offset, collectableCount);
    }

    private void Update()
    {
        followTransform.position = originTransform.position;
        followTransform.rotation = originTransform.rotation;
    }

    private void AddCollector()
    {
        if (!TryGetComponent(out Interactor c))
            gameObject.AddComponent<Interactor>();
    }

    public void CollectIngredient(Ingredient ingredient)
    {
        if (ingredients.Count == IngredientPositions.Count) return;

        if (ingredient.IngredientValue > chefLevel)
        {
            ingredient.StartText();
            return;
        }
        if (ingredient.IngredientValue > highestValue) highestValue = ingredient.IngredientValue;

        ingredient.collected = true;

        EventManager.OnCollection.Invoke(ingredient.ID);

        Transform ingredientTransform = IngredientPositions[ingredients.Count].transform;
        Transform followTransform;

        if (ingredients.Count > 0)
        {
            followTransform = ingredients[ingredients.Count - 1].topTransform;
        }
        else
        {
            followTransform = IngredientPositions[ingredients.Count].transform;
        }

        ingredient.carryTransform = followTransform;
        IngredientPositions[ingredients.Count].full = true;

        StartCoroutine(Collection(ingredient, followTransform));
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

        ing.gameObject.layer = LayerMask.NameToLayer("Food");
        ing.SetRigidColl(false);
    }

    public void UpgradeCollection()
    {
        IngredientPositions.AddNewPosition();
    }

    public void UpgradeChef()
    {
        chefLevel++;
    }

    public void UpgradeIncome()
    {
        incomeMult++;
    }
}