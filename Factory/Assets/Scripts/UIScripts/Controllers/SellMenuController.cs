using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SellMenuController : MonoBehaviour
{
    [System.Serializable]
    public class SellableResources
    {
        public WorkerBonusTypes type;
        public int sell_amount = 0;
    }

    [SerializeField] private List<SellableResources> sellables = new List<SellableResources>();
    [SerializeField] private TextMeshProUGUI sell_menu_text;
    [SerializeField] private TextMeshProUGUI total_text;

    private int sell_round_money = 0;

    public event Action<List<SellableResources>> onSellCall;

    public void initializeSellOff()
    {
        ResetUI();
        onSellCall?.Invoke(sellables);
    }

    public void displaySelloff(WorkerBonusTypes type, int num_resources, int money_made)
    {
        sell_menu_text.text += $"{num_resources} {type}: <color=#099813>${money_made}</color>\n";
        sell_round_money += money_made;
        total_text.text = "$" + sell_round_money.ToString();
    }

    private void ResetUI()
    {
        sell_round_money = 0;
        sell_menu_text.text = "";
        total_text.text = "";
    }
}
