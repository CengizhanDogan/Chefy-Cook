using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour, IInteractable
{
    public void Interact(Interactor interactor)
    {
        EventManager.OnGemCollected.Invoke(transform.position, () => { });
    }
}
