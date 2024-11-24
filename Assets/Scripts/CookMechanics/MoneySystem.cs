using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    private double money = 0;
    [SerializeField] private TextMeshProUGUI moneyCanvas;

    public void EarnMoney(double earned)
    {
        money += earned;
        if(moneyCanvas != null)
        {
            moneyCanvas.text = $"Your money: {money}";
        }
    }

    public void SpendMoney(double spent)
    {
        money -= spent;
        if (moneyCanvas != null)
        {
            moneyCanvas.text = $"Your money: {money}";
        }
    }

    public bool HaveEnoughMoney(double priceToCompare)
    {
        if(money >= priceToCompare)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}