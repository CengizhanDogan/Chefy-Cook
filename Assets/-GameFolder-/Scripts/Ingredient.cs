using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ingredient : MonoBehaviour, IInteractable
{
    public int ID;

    private IngredientManager ingredientManager;

    IngredientPositions ingredientPositions;
    public int MyIndex { get; private set; }

    private bool setting;

    [SerializeField] private int ingredientValue;
    public int IngredientValue { get { return ingredientValue; } }
    public int listOrder;
    public bool collected;

    public CookingType cookingType;

    public Transform carryTransform;

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

                    carryTransform = ingredientPositions[MyIndex - 1].transform;

                    MyIndex--;
                }
            }
    }

    public IEnumerator ResetPos()
    {
        while (Vector3.Distance(transform.position, carryTransform.position) > 0.01f)
        {
            Debug.Log("Reset");
            transform.position = Vector3.Lerp(transform.position, carryTransform.position, 10f * Time.deltaTime);
            yield return null;
        }
    }
}
