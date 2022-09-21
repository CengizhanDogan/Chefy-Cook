using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IngredientManager))]
public class Interactor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IInteractable collectable = other.GetComponentInChildren<IInteractable>();
        if (collectable != null)
        {
            collectable.Interact(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        IInteractable collectable = collision.collider.GetComponentInChildren<IInteractable>();
        if (collectable != null)
        {
            collectable.Interact(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IExitable exitable = other.GetComponentInChildren<IExitable>();
        if (exitable != null)
        {
            exitable.Exit();
        }
    }
}
