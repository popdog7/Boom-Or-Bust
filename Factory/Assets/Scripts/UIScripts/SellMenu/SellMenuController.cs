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

    public event Action<List<SellableResources>> onSellCall;

    public void initializeSellOff()
    {
        onSellCall?.Invoke(sellables);
    }

    public void displaySelloff(WorkerBonusTypes type, int num_resources, int money_made)
    {
        sell_menu_text.text += "Sold " + num_resources + " " + type + " Making" + money_made + "\n";
    }

    public void transitionToUpgradePhase()
    {

    }


}
