using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IngredientManager))]
public class Collector : MonoBehaviour
{
    public IngredientManager IngredientManager { get; private set; }

    public void SetManager(IngredientManager ingredientManager)
    {
        this.IngredientManager = ingredientManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        ICollectable collectable = other.GetComponentInChildren<ICollectable>();
        if (collectable != null)
        {
            collectable.GetCollected(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ICollectable collectable = collision.collider.GetComponentInChildren<ICollectable>();
        if (collectable != null)
        {
            collectable.GetCollected(this);
        }
    }
}
