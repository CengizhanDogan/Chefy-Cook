using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Ingredient : MonoBehaviour, IInteractable
{
    public int ID;

    private IngredientManager ingredientManager;

    private IngredientPositions ingredientPositions;

    public Transform topTransform;

    private Rigidbody rb;
    private Rigidbody Rb => rb == null ? rb = GetComponentInParent<Rigidbody>() : rb;

    private Collider coll;
    private Collider Coll => coll == null ? coll = GetComponentInParent<Collider>() : coll;
    public int MyIndex { get; private set; }

    [SerializeField] private int ingredientValue;
    public int IngredientValue { get { return ingredientValue; } }
    public int listOrder;
    public bool collected;

    public CookingType cookingType;

    public Transform carryTransform;

    private void Start()
    {
        CreateTopPos();

        ingredientManager = IngredientManager.Instance;
    }

    private void CreateTopPos()
    {
        topTransform = new GameObject().transform;
        topTransform.SetParent(transform);
        topTransform.gameObject.name = "TopTransform";
        Vector3 topPos = transform.position;
        topPos.y = Coll.bounds.extents.y + 0.75f;
        topTransform.position = topPos;
    }

    public void Interact(Interactor interactor)
    {
        if (collected) return;

        ingredientManager.CollectIngredient(this);
        ingredientPositions = ingredientManager.IngredientPositions;
        MyIndex = ingredientManager.Ingredients.IndexOf(this);
    }

    private void Update()
    {
        SetNewPos();
    }

    private void SetNewPos()
    {
        if (ingredientManager.Ingredients.Contains(this))
        {
            if (MyIndex == 0) return;

            if (!ingredientPositions[MyIndex - 1].full)
            {
                ingredientPositions[MyIndex].full = false;
                ingredientPositions[MyIndex - 1].full = true;

                carryTransform = ingredientPositions[MyIndex - 1].transform;

                MyIndex--;
            }
        }
    }

    public void SetRigidColl(bool set)
    {
        Rb.isKinematic = set;
        Coll.isTrigger = set;
    }
}
