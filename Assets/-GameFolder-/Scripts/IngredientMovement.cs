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
            combination[i].SetRigidColl(true);
            Transform moveTransform = combination[i].transform;
            combination[0].StopAllCoroutines();
            Destroy(combination[i]);
            moveTransform.DOMove(target.position, 0.5f);
        }

        EventManager.OnCombination.Invoke(combination, target);
    }

    public void MoveIngredient(Ingredient ingredient)
    {
        ingredient.SetRigidColl(true);
        Transform moveTransform = ingredient.transform;
        ingredient.StopAllCoroutines();
        Destroy(ingredient);
        moveTransform.DOMove(target.position, 0.5f);
    }
}
