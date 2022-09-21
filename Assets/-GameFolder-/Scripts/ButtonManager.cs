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
        EventManager.OnGemChange.AddListener(CheckPrice);
    }
    private void OnDisable()
    {
        EventManager.OnGemChange.RemoveListener(CheckPrice);
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

        text.text = buttonPrice.ToString();
    }
    public void CheckPrice()
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
}
