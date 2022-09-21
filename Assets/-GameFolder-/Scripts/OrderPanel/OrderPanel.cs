using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OrderPanel : Singleton<OrderPanel>
{
    [SerializeField] private Transform posOne;
    [SerializeField] private Transform posTwo;

    private List<Order> orders = new List<Order>();

    public List<Order> currentOrders = new List<Order>();

    private void OnEnable()
    {
        LevelManager.Instance.OnLevelFinish.AddListener(ResetPanel);
    }

    private void OnDisable()
    {
        LevelManager.Instance.OnLevelFinish.RemoveListener(ResetPanel);
    }

    private void ResetPanel()
    {
        foreach (var order in currentOrders)
        {
            Destroy(order.gameObject);
        }

        currentOrders.Clear();
    }
    public void SetList(List<Order> orders)
    {
        foreach (var order in orders)
        {
            this.orders.Add(order);
        }
    } 

    public void CreateOrder()
    {
        currentOrders.Add(Instantiate(orders[Random.Range(0, orders.Count)], posTwo.position, Quaternion.identity, transform));

        Vector3 scale = currentOrders[currentOrders.Count - 1].transform.localScale;
        currentOrders[currentOrders.Count - 1].transform.localScale = Vector3.zero;

        currentOrders[currentOrders.Count - 1].transform.DOScale(scale, 0.25f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            currentOrders[0].transform.DOMove(posOne.position, 0.25f).OnComplete(() =>
            {
                if (currentOrders.Count <= 1) CreateOrder();
            });
        });
    }
}
