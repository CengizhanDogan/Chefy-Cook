using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IngredientPanel : MonoBehaviour
{
    [SerializeField] private int panelID;

    private void OnEnable()
    {
        EventManager.OnCollection.AddListener(Check);
    }

    private void OnDisable()
    {
        EventManager.OnCollection.RemoveListener(Check);
    }

    private void Check(int id)
    {
        if (id == panelID)
        {
            transform.DOLocalMoveY(0, 0.5f);
        }
    }
}
