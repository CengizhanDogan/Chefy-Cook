using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private List<Order> orders = new List<Order>();

    private void Start()
    {
        OrderPanel.Instance.SetList(orders);
        OrderPanel.Instance.CreateOrder();
    }
}
