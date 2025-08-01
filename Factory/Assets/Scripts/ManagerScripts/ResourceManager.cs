
using System;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [Header("UI Controllers")]
    [SerializeField] private MoneyUI money_ui;
    [SerializeField] private ResourceUIController ui_controller;
    [SerializeField] private WorkerPurchaseController purchase_controller;
    [SerializeField] private ResearchPurchaseController research_controller;

    [Header("Bank")]
    [SerializeField] private ResourceBank bank;

    [Header("Spawner")]
    [SerializeField] private WorkerSpawner worker_spawner;

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
        worker_spawner.onWorkerCreated += connectWorker;
        purchase_controller.onAttemptWorkerPurchase += bank.attemptPurchase;
        bank.signalPurchaseOutcome += purchase_controller.determinePurchaseOutcome;
        bank.updateMoneyUI += money_ui.setMoney;
        purchase_controller.onAttemptWorkerReroll += bank.attemptReroll;
        bank.signalRerollOutcome += purchase_controller.reroll;
        purchase_controller.onWorkerSuccesfulPurchase += worker_spawner.addWorker;
        research_controller.onResearchUnlockAttempt += bank.attemptUnlockResearch;
        bank.signalResearchOutcome += research_controller.researchUnlockOutcome;
    }


}
