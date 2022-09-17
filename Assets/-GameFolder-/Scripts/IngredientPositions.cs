using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPositions
{
    private Transform origin;
    private Vector3 offset;
    private int count;

    private List<IngredientPosition> positions = new List<IngredientPosition>();

    public IngredientPositions(Transform origin, Vector3 offset, int count)
    {
        this.origin = origin;
        this.offset = offset;
        this.count = count;

        CreateList();
    }

    private void CreateList()
    {
        for (int i = 0; i < count; i++)
        {
            AddNewPosition(i);
        }
    }

    public void AddNewPosition()
    {
        AddNewPosition(positions.Count);
    }

    public void AddNewPosition(int order)
    {
        Vector3 position = origin.position + (offset * order);

        var positionRef = new GameObject();
        positionRef.name = $"positionReferance({order + 1})";
        positionRef.transform.position = position;
        positionRef.transform.SetParent(origin);
        positionRef.transform.localEulerAngles = Vector3.zero;

        var ingredientPosition = new IngredientPosition(positionRef.transform);
        positions.Add(ingredientPosition);
    }
    public IngredientPosition this[int index]
    {
        get { return positions[index]; }
    }

    public int Count
    {
        get { return positions.Count; }
    }
}

public class IngredientPosition
{
    public Transform transform;
    public bool full;

    public IngredientPosition(Transform transform)
    {
        this.transform = transform;
    }
}
