using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IngredientManager))]
public class Collector : MonoBehaviour
{
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
