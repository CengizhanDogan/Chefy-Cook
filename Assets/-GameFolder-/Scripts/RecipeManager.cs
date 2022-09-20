using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RecipeManager : MonoBehaviour
{
    [SerializeField] private List<Recipe> recipes = new List<Recipe>();

    private void OnEnable()
    {
        EventManager.OnCombination.AddListener(SpawnMeal);
    }
    private void OnDisable()
    {
        EventManager.OnCombination.RemoveListener(SpawnMeal);
    }

    private void SpawnMeal(int recipeID, Transform spawnTransform)
    {
        foreach (var recipe in recipes)
        {
            if (recipe.ID == recipeID)
            {
                SendToTable(Instantiate(recipe, spawnTransform.position, Quaternion.identity, transform));
            }
        }
    }

    private void SendToTable(Recipe recipe)
    {
        Transform meal = recipe.transform;
        Vector3 scale = meal.localScale;
        meal.transform.localScale = Vector3.zero;
        meal.DOScale(scale, 0.25f).SetEase(Ease.OutBack);

        // ...
    }
}
