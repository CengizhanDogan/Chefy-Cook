using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStation : MonoBehaviour, IInteractable, IExitable
{
    public void Interact(Interactor interactor)
    {
        UpgradeManager.Instance.SetButtons(true);
    }
    public void Exit()
    {
        UpgradeManager.Instance.SetButtons(false);
    }
}
