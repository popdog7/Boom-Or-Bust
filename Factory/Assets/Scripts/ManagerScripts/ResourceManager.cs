
using System;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private ResourceBank bank;
    [SerializeField] private ResourceUIController ui_controller;
    [SerializeField] private WorkerPurchaseController purchase_controller;
    [SerializeField] private MoneyUI money_ui;
    private ResourceData data;

    private void Awake()
    {
        data = new();
        bank.importResourceData(data);
        connectUI();
    }

    public void connectWorker(Worker employee)
    {
        employee.onCreateProduct += bank.addResource;
    }

    public void connectUI()
    {
        bank.updateResourceUI += ui_controller.updateElementUI;
        purchase_controller.onAttemptWorkerPurchase += bank.attemptPurchase;
        bank.signalPurchaseOutcome += purchase_controller.determinePurchaseOutcome;
        bank.updateMoneyUI += money_ui.setMoney;
        purchase_controller.onAttemptWorkerReroll += bank.attemptReroll;
        bank.signalRerollOutcome += purchase_controller.reroll;
    }


}
