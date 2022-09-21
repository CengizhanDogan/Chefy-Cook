using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class IngredientPanel : MonoBehaviour
{
    [SerializeField] private int panelID;
    private IngredientManager ingredientManager;
    private RectTransform rectTransform;
    private Vector3 startPos;

    private bool resetPos;

    private void OnEnable()
    {
        EventManager.OnCollection.AddListener(Check);
    }

    private void OnDisable()
    {
        EventManager.OnCollection.RemoveListener(Check);
    }
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.localPosition;
        ingredientManager = IngredientManager.Instance;
    }

    private void Update()
    {
        if (resetPos) return;

        if (!ingredientManager.Ingredients.Any(i => i.ID == panelID))
        {
            Debug.Log("Reset");
            rectTransform.DOLocalMove(startPos, 0.5f);
            resetPos = true;
        }
    }

    private void Check(int id)
    {
        if (id == panelID)
        {
            rectTransform.DOLocalMoveY(0, 0.5f);
            resetPos = false;
        }
    }
}
