using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Ingredient : MonoBehaviour, IInteractable
{
    private IngredientManager ingredientManager;

    IngredientPositions ingredientPositions;
    public int MyIndex;

    private bool setting;

    [SerializeField] private int ingredientValue;
    public int IngredientValue { get { return ingredientValue; } }
    public int listOrder;
    public bool collected;

    public CookingType cookingType;

    private void Start()
    {
        ingredientManager = IngredientManager.Instance;
    }
    public void Interact(Interactor interactor)
    {
        if (collected) return;

        ingredientManager.CollectIngredient(this);
        ingredientPositions = ingredientManager.IngredientPositions;
        MyIndex = ingredientManager.Ingredients.IndexOf(this);
    }
    private void Update()
    {
        SetNewPos();
    }

    private void SetNewPos()
    {
        if (!setting)

            if (ingredientManager.Ingredients.Contains(this))
            {
                if (MyIndex == 0) return;

                if (!ingredientPositions[MyIndex - 1].full)
                {
                    setting = true;
                    ingredientPositions[MyIndex].full = false;
                    ingredientPositions[MyIndex - 1].full = true;

                    transform.DOMove(ingredientPositions[MyIndex - 1].transform.position, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        setting = false;
                    });

                    MyIndex--;
                }
            }
    }
}
