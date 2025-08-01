using System;
using TMPro;
using UnityEngine;

public class ShadyUIElement : MonoBehaviour
{
    [SerializeField] ShadyDealSO deal;
    [SerializeField] TextMeshProUGUI debt_text;
    [SerializeField] TextMeshProUGUI money_text;
    [SerializeField] TextMeshProUGUI button_text;

    public event Action<int, int> onAcceptedShadyDeal;

    private bool can_accpet = true;

    public void initalize()
    {
        debt_text.text = "$" + deal.debt.ToString();
        money_text.text = "$" + deal.money.ToString();
    }

    public void onAccepted()
    {
        if(can_accpet)
        {
            button_text.text = "Taken";
            can_accpet = false;
            onAcceptedShadyDeal(deal.debt, deal.money);
        }
        
    }

}
