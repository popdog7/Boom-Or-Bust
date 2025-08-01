
using System;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [Header("UI Controllers")]
    [SerializeField] private MoneyUI money_ui;
    [SerializeField] private ResourceUIController ui_controller;
    [SerializeField] private WorkerPurchaseController purchase_controller;
    [SerializeField] private ResearchPurchaseController research_controller;
    [SerializeField] private ShadyPurchaseController shady_controller;
    [SerializeField] private SellMenuController sell_controller;
    [SerializeField] private UIStateController ui_state_controller;

    [Header("Timers")]
    [SerializeField] private FactoryTimer factory_timer;

    [Header("Bank")]
    [SerializeField] private ResourceBank bank;

    [Header("Spawner")]
    [SerializeField] private WorkerSpawner worker_spawner;

    [Header("Game State Manager")]
    [SerializeField] private GameStateManager state_manager;

    private ResourceData data;

    private void Awake()
    {
        data = new();
        bank.importResourceData(data);
        connectUI();
        connectStates();
    }

    public void connectWorker(Worker employee)
    {
        employee.onCreateProduct += bank.addResource;
        state_manager.signalWorkerPause += employee.pauseProdcution;
        state_manager.signalWorkerUnpause += employee.unpauseProdcution;
    }

    public void connectUI()
    {
        bank.updateResourceUI += ui_controller.updateElementUI;
        worker_spawner.onWorkerCreated += connectWorker;
        purchase_controller.onAttemptWorkerPurchase += bank.attemptPurchase;
        bank.signalPurchaseOutcome += purchase_controller.determinePurchaseOutcome;
        bank.updateMoneyUI += money_ui.setMoney;
        bank.updateDebtUI += money_ui.setDebt;
        purchase_controller.onAttemptWorkerReroll += bank.attemptReroll;
        bank.signalRerollOutcome += purchase_controller.reroll;
        purchase_controller.onWorkerSuccesfulPurchase += worker_spawner.addWorker;
        research_controller.onResearchUnlockAttempt += bank.attemptUnlockResearch;
        bank.signalResearchOutcome += research_controller.researchUnlockOutcome;
        shady_controller.onAcceptedShadyDeal += bank.acceptShadyDeal;
        sell_controller.onSellCall += bank.sellResources;
        bank.updateSellScreen += sell_controller.displaySelloff;

    }

    public void connectStates()
    {
        factory_timer.onTimerFinished += state_manager.onSellState;
        state_manager.signalSellMenuDisplay += sell_controller.initializeSellOff;
        state_manager.signalTimerReset += factory_timer.restartPhase;
        state_manager.signalUIChange += ui_state_controller.changeUIGroup;
    }


}
