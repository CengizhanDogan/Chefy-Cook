using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class StationArrow : MonoBehaviour
{
    [SerializeField] CookingType type;
    [SerializeField] private GameObject gfx;

    private IngredientManager ingredientManager;

    private Vector3 scale;
    private bool doOnce = true;
    private bool animating;
    private Vector3 startPos;
    private Vector3 endPos;

    void Start()
    {
        ingredientManager = IngredientManager.Instance;

        scale = gfx.transform.localScale;
        gfx.transform.localScale = Vector3.zero;
        startPos = transform.position;
        endPos = startPos; endPos.y += 1f;
    }
    void Update()
    {
        if (transform.position.y >= endPos.y - 0.001)
        {
            animating = true;
        }
        if (transform.position.y <= startPos.y + 0.001)
        {
            animating = false;
        }
        if (transform.position.y != endPos.y && !animating)
        {
            
            var newPos = transform.position;
            newPos.y = startPos.y + 1f;
            transform.position = Vector3.Lerp(transform.position, newPos, 10f * Time.deltaTime);
        }
        if (transform.position.y != startPos.y && animating)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, 10f * Time.deltaTime);
        }

        if (ingredientManager.Ingredients.Count > 0 && 
            ingredientManager.Ingredients.Any(i => i.cookingType == type))
        {
            if (gfx.transform.localScale.x <= 0f)
            {
                DOTween.Kill(gfx.transform);
                gfx.transform.DOScale(scale, 0.5f).SetId("Size").SetEase(Ease.OutBack);
                doOnce = true;
            }
        }
        else
        {
            if (doOnce)
            {
                doOnce = false;
                DOTween.Kill(gfx.transform);
                gfx.transform.DOScale(Vector3.zero, 0.25f).SetId("Size").SetEase(Ease.Linear);
            }
        }
    }
}
