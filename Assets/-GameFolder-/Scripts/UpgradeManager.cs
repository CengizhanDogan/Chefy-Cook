using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : Singleton<UpgradeManager>
{
    [SerializeField] private List<ButtonManager> buttons = new List<ButtonManager>();

    [SerializeField] private GameObject enabler;

    [SerializeField] private int chefPrice;
    [SerializeField] private int collectPrice;
    [SerializeField] private int incomePrice;

    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStart.AddListener(SetButtons);
        EventManager.OnGemChange.AddListener(Debugger);
    }
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStart.RemoveListener(SetButtons);
        EventManager.OnGemChange.RemoveListener(Debugger);
    }
    private void SetButtons()
    {
        IngredientManager ingredientManager = IngredientManager.Instance;

        if (ingredientManager)
        {
            buttons[0].button.onClick.AddListener(ingredientManager.UpgradeChef);
            buttons[0].SetBasePrice(chefPrice);
            buttons[1].button.onClick.AddListener(ingredientManager.UpgradeCollection);
            buttons[1].SetBasePrice(collectPrice);
            buttons[2].button.onClick.AddListener(ingredientManager.UpgradeIncome);
            buttons[2].SetBasePrice(incomePrice);
        }

        foreach (ButtonManager button in buttons)
        {
            button.CheckPrice();
            button.button.onClick.AddListener(button.SpendMoney);
        }
    }
    public void SetButtons(bool set)
    {
        enabler.SetActive(set);
    }

    private void Debugger()
    {
        foreach (ButtonManager button in buttons)
        {
            button.CheckPrice();
        }
    }
}
