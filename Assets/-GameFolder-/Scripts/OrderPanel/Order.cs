using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Order : MonoBehaviour
{
    public int orderID;
    private OrderPanel orderPanel;

    private void Start()
    {
        orderPanel = OrderPanel.Instance;
    }

    private void OnEnable()
    {
        EventManager.OnDelivery.AddListener(CheckOrder);
    }
    private void OnDisable()
    {
        EventManager.OnDelivery.RemoveListener(CheckOrder);
    }

    private void CheckOrder(int id)
    {
        if (orderPanel.currentOrders.IndexOf(this) != 0)
        {
            if (orderPanel.currentOrders[0].orderID == orderID)
            {
                return;
            }
        }
        if (id == orderID)
        {
            DeliverManager.Instance.SpawnMoney(20);
            CloseOrder();
        }
    }

    private void CloseOrder()
    {
        transform.DOScale(Vector3.zero, 0.1f).OnComplete(() =>
        {
            orderPanel.currentOrders.Remove(this);
            Destroy(gameObject);
            orderPanel.CreateOrder();
        });
    }
}
