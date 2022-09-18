using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IngredientMovement : MonoBehaviour
{
    [SerializeField] private Transform target;

    public void MoveIngredient(Combination combination)
    {
        for (int i = 0; i < combination.Count; i++)
        {
            combination[i].transform.DOMove(target.position, 0.5f);
        }

        EventManager.OnCombination.Invoke();
    }

    public void MoveIngredient(Ingredient ingredient)
    {
        ingredient.transform.DOMove(target.position, 0.5f);

        EventManager.OnCombination.Invoke();
    }
}
