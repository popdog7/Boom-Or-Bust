using System;
using System.Collections.Generic;
using UnityEngine;

public class ShadyPurchaseController : MonoBehaviour
{
    [SerializeField] List<ShadyUIElement> deals;

    public event Action<int, int> onAcceptedShadyDeal;

    private void Awake()
    {
        foreach(var ui in deals)
        {
            ui.onAcceptedShadyDeal += acceptedDeal;
            ui.initalize();
        }
    }

    private void acceptedDeal(int debt, int amount)
    {
        onAcceptedShadyDeal?.Invoke(debt, amount);
    }
}
