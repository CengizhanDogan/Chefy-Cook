using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour, ICollectable
{
    public int IngredientValue { get; private set; }
    public bool collected;
    public void GetCollected(Collector collector)
    {
        if (collected) return;

        collector.IngredientManager.CollectIngredient(this);
    }
}
