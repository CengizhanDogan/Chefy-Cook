using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button button;
    [SerializeField] private Text text;
    public int buttonPrice;
    public int basePrice;

    private void OnEnable()
    {
        EventManager.OnGemCollected.AddListener(CheckPrice);
        EventManager.OnGemSpend.AddListener(CheckPrice);
    }
    private void OnDisable()
    {
        EventManager.OnGemCollected.RemoveListener(CheckPrice);
        EventManager.OnGemSpend.RemoveListener(CheckPrice);
    }
    public void SetBasePrice(int basePrice)
    {
        this.basePrice = basePrice;
        buttonPrice = basePrice;
    }
    public void SpendMoney()
    {
        EventManager.OnGemSpend.Invoke(buttonPrice);
        buttonPrice += basePrice;
    }
    public void CheckPrice(int a)
    {
        if (PlayerPrefs.GetInt(PlayerPrefKeys.Coin) < buttonPrice)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }

        text.text = buttonPrice.ToString();
    }
    private void CheckPrice(Vector3 a, Action b)
    {
        CheckPrice(0);
    }
}
