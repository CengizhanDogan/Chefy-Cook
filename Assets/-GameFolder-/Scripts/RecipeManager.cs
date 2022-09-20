using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RecipeManager : MonoBehaviour
{
    [SerializeField] private Transform spawnTransform;

    [SerializeField] private List<Recipe> recipes = new List<Recipe>();

    private void OnEnable()
    {
        EventManager.OnCombination.AddListener(SpawnMeal);
    }
    private void OnDisable()
    {
        EventManager.OnCombination.RemoveListener(SpawnMeal);
    }

    private void SpawnMeal(Combination combination)
    {
        foreach (var recipe in recipes)
        {
            if (recipe.ID == combination.CombinationID)
            {
                SendToTable(Instantiate(recipe, spawnTransform.position, Quaternion.identity, transform));
            }
        }
    }

    private void SendToTable(Recipe recipe)
    {
        // ...
    }
}
