using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private int money;
    [SerializeField] private TMP_Text moneyText;
    public int Money { get => money; set => SetMoney(value); }

    private void Awake()
    {
        SetMoney(money);
    }

    private void SetMoney(int value)
    {
        money = value;
        moneyText.text = "$" + value;
    }

    public bool HasEnoughMoneyForPurchase(int cost)
    {
        return money - cost >= 0;
    }
}
