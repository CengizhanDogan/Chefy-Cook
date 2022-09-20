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
            MoveIngredient(combination[i], true);
        }

        EventManager.OnCombination.Invoke(combination.CombinationID, target);
    }

    public void MoveIngredient(Ingredient ingredient, bool combination)
    {
        ingredient.SetRigidColl(true);
        Transform moveTransform = ingredient.transform;
        ingredient.StopAllCoroutines();
        Destroy(ingredient);
        moveTransform.DOMove(target.position, 0.5f).OnComplete(() =>
        {
            moveTransform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
            {
                Destroy(ingredient.gameObject);
            });
        });

        if (!combination)
        {
            EventManager.OnCombination.Invoke(ingredient.ID, target);
        }
    }
}