using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorkerPurchaseController : MonoBehaviour
{
    [Header("UI inputs")]
    [SerializeField] private List<WorkerStats> workerStats = new List<WorkerStats>();
    [SerializeField] private List<workerPurchaseUI> worker_purchase_ui;

    [Header("Reroll Attributs")]
    [SerializeField] private int base_cost = 100;
    [SerializeField] private int cost_multiplier = 2;
    [SerializeField] private TextMeshProUGUI cost_text;
    private int cost;

    private bool currently_rolling = false;

    public event Action<workerPurchaseUI, int> onAttemptWorkerPurchase;
    public event Action<int> onAttemptWorkerReroll;

    private void Awake()
    {
        foreach(var ui_element in worker_purchase_ui)
        {
            ui_element.setWorkerUI(chooseWorker());
            ui_element.onPurchase += (ui, amount) => onAttemptWorkerPurchase?.Invoke(ui, amount);
        }

        cost = base_cost;
        cost_text.text = "$" + cost;
    }

    private WorkerStats chooseWorker()
    {
        int index = UnityEngine.Random.Range(0, workerStats.Count);
        return workerStats[index];
    }

    public void determinePurchaseOutcome(workerPurchaseUI ui ,bool outcome)
    {
        if (outcome)
        {
            ui.markPurchaseSuccess();
        }
        else
        {
            ui.markPurchaseFailure();
        }
    }

    public void attemptReroll()
    {
        currently_rolling = true;
        onAttemptWorkerReroll?.Invoke(cost);
    }

    public void reroll(bool outcome)
    {

        if (!currently_rolling)
            return;

        if (outcome)
        {
            foreach (var ui_element in worker_purchase_ui)
            {
                ui_element.setWorkerUI(chooseWorker());
            }
            cost *= cost_multiplier;
            cost_text.text = "$" + cost;
            currently_rolling = false;
        }
        else
        {
            currently_rolling = false;
        }
    }
}
