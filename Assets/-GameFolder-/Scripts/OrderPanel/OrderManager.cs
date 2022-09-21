using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private List<Order> orders = new List<Order>();

    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStart.AddListener(SetOrderPanel);
    }
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStart.RemoveListener(SetOrderPanel);
    }
    private void SetOrderPanel()
    {
        OrderPanel.Instance.SetList(orders);
        OrderPanel.Instance.CreateOrder();
    }
}
