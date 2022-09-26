using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterArrow : MonoBehaviour
{
    private IngredientManager ingredientManager;
    [SerializeField] private Transform targetPos;

    [SerializeField] private GameObject gfx;

    private Vector3 scale;
    private bool doOnce;

    private void Start()
    {
        scale = gfx.transform.localScale;
        gfx.transform.localScale = Vector3.zero;
        ingredientManager = IngredientManager.Instance;
    }
    void Update()
    {
        Activete();
        Movement();
        Clamp();
    }

    private void Activete()
    {
        if (Vector3.Distance(transform.position, targetPos.position) > 2.5f
            && ingredientManager.Ingredients.Count > 0)
        {
            if (gfx.transform.localScale.x <= 0f)
            {
                DOTween.KillAll(gfx.transform);
                gfx.transform.DOScale(scale, 0.5f).SetEase(Ease.OutBack);
                doOnce = true;
            }
        }
        else
        {
            if (gfx.transform.localScale.x >= scale.x && doOnce)
            {
                doOnce = false;
                DOTween.KillAll(gfx.transform);
                gfx.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.Linear);
            }
        }
    }

    private void Movement()
    {
        transform.LookAt(targetPos);
        transform.position = Vector3.Lerp(transform.position, targetPos.position, 2f * Time.deltaTime);
    }
    private void Clamp()
    {
        var pos = Camera.main.WorldToViewportPoint(transform.localPosition);
        pos.x = Mathf.Clamp(pos.x, 0.20f, 0.85f);
        pos.y = Mathf.Clamp(pos.y, 0.15f, 0.85f);
        var worldPos = Camera.main.ViewportToWorldPoint(pos);

        //worldPos.z = worldPos.y;
        worldPos.y = 0f;
        transform.localPosition = worldPos;
    }

}
