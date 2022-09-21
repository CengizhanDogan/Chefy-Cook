using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeliverManager : Singleton<DeliverManager>
{
    [SerializeField] private Transform placement;
    [SerializeField] private Transform delivery;

    [SerializeField] private Transform spawnTransform;

    [SerializeField] private GameObject moneyPrefab;

    [SerializeField] private Renderer rend;

    [SerializeField] private float rendValue;

    private void Update()
    {
        Vector2 offset = rend.materials[1].mainTextureOffset;
        offset.y += rendValue;

        rend.materials[1].mainTextureOffset = offset;
    }

    public void DeliverOrder(Recipe recipe)
    {
        recipe.transform.DOJump(placement.position, 2f, 1, 0.5f).OnComplete(() =>
        {
            recipe.transform.DOMove(delivery.position, 1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                EventManager.OnDelivery.Invoke(recipe.ID);
                SpawnMoney(recipe.value);
                Destroy(recipe.gameObject);
            });
        });
    }

    public void SpawnMoney(int moneyCount)
    {
        moneyCount *= IngredientManager.Instance.incomeMult;
        moneyCount /= 5;
        for (int i = 0; i < moneyCount; i++)
        {
            Vector3 randomPos = spawnTransform.position;

            randomPos.x = Random.Range(randomPos.x - 1f, randomPos.x + 1f);
            randomPos.z = Random.Range(randomPos.z - 1f, randomPos.z + 1f);

            var money = PoolingSystem.Instance.InstantiateAPS("Money", transform.position);

            money.transform.DOJump(randomPos, 2f, 1, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                money.transform.DORotate(Vector3.up * 90, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
            });
        }
    }
}
