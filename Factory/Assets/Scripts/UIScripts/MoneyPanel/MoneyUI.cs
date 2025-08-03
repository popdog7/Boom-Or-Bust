using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI current_money;
    [SerializeField] private TextMeshProUGUI current_debt;

    public void setMoney(int amount)
    {
        current_money.text = "$" + amount.ToString();
    }

    public void setDebt(int amount)
    {
        current_debt.text = "$" + amount.ToString();
    }
}
