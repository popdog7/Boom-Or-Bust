using Mono.Cecil;
using System;
using System.Collections.Generic;
using UnityEngine;
using static ItemCostSO;

public class ResourceBank : MonoBehaviour
{
    [SerializeField] bool debug_messages = false;

    private ResourceData data;

    public event Action<WorkerBonusTypes, int> updateResourceUI;
    public event Action<workerPurchaseUI, bool> signalPurchaseOutcome;
    public event Action<bool> signalRerollOutcome;
    public event Action<bool> signalResearchOutcome;
    public event Action<int> updateMoneyUI;
    public event Action<int> updateDebtUI;

    public void importResourceData(ResourceData data)
    {
        this.data = data;
    }

    private void Start()
    {
        updateDebtUI?.Invoke(data.debt);
        updateMoneyUI?.Invoke(data.money);
    }

    #region Resource Type Management

    public void addResource(WorkerBonusTypes type, int amount, ItemCostSO cost)
    {
        // if it cost something
        //we check storage if we have that
        //if we dont we send pause else remove from storage
        // then add to storage crafted wont have limtit
        if (cost.hasCost())
        {
            if (checkCost(cost.item_cost))
            {
                foreach (var resource_type in cost.item_cost)
                {
                    removeFromStorage(resource_type.type, resource_type.cost);
                }

                addToStorage(type, amount);
            }
        }
        else
        {
            //if dosent cost check storage for max
            //if max send pause signal
            //else add to storage
            addToStorage(type, amount);
        }
    }

    //technically this dosent need to be an if
    private void addToStorage(WorkerBonusTypes type, int amount)
    {
        if (data.resourceStorage.TryGetValue(type, out int current_amount))
        {
            data.resourceStorage[type] = current_amount + amount;
        }
        else
        {
            data.resourceStorage[type] = amount;
        }
        updateResourceUI?.Invoke(type, data.resourceStorage[type]);

        additonMessage(type, current_amount, amount);
    }

    //not to self this might result in a bug if it happens to soon with another one where it becomes negative
    private void removeFromStorage(WorkerBonusTypes type, int amount)
    {
        beforeRemovalMessage(amount);

        data.resourceStorage[type] -= amount;
        updateResourceUI?.Invoke(type, data.resourceStorage[type]);

        afterRemovalMessage(type, amount);
    }

    private bool checkCost(List<ResourceCost> item_cost)
    {

        return item_cost.TrueForAll(resource => data.resourceStorage.TryGetValue(resource.type, out int resource_amount) && resource.cost <= resource_amount);
    }

    #endregion

    #region Money Management

    public void attemptPurchase(workerPurchaseUI ui, int amount)
    {
        if(data.money >= amount)
        {
            updateMoney(-amount);
            signalPurchaseOutcome?.Invoke(ui, true);
        }

        signalPurchaseOutcome?.Invoke(ui, false);
    }

    public void attemptReroll(int amount)
    {
        if (data.money >= amount)
        {
            updateMoney(-amount);
            signalRerollOutcome?.Invoke(true);
        }

        signalRerollOutcome?.Invoke(false);
    }

    public void attemptUnlockResearch(int amount)
    {
        if (data.money >= amount)
        {
            updateMoney(-amount);
            signalResearchOutcome?.Invoke(true);
        }

        signalResearchOutcome?.Invoke(false);
    }

    public void acceptShadyDeal(int debt, int money)
    {
        updateDebt(debt);
        updateMoney(money);
    }

    private void updateMoney(int amount)
    {
        data.money += amount;
        updateMoneyUI?.Invoke(data.money);
    }

    private void updateDebt(int amount)
    {
        data.debt += amount;
        updateDebtUI?.Invoke(data.debt);
    }

    #endregion

    #region debug messages

    public void beforeRemovalMessage(int amount)
    {
        if(debug_messages)
            Debug.Log("Before Removal: " + amount);
    }

    public void afterRemovalMessage(WorkerBonusTypes type, int amount)
    {
        if (debug_messages)
        {
            Debug.Log("Removed " + amount + " " + type);
            Debug.Log("After Removal: " + amount);
        }
    }

    public void additonMessage(WorkerBonusTypes type, int current_amount, int amount)
    {
        if (debug_messages)
            Debug.Log(type + " : " + current_amount + " + " + amount);
    }

    #endregion
}
