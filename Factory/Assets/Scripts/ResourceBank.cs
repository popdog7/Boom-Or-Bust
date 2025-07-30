using Mono.Cecil;
using System;
using System.Collections.Generic;
using UnityEngine;
using static ItemCostSO;

public class ResourceBank : MonoBehaviour
{
    private ResourceData data;

    public event Action<WorkerBonusTypes, int> updateResourceUI;

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
        Debug.Log(type + " : " + current_amount + " + " + amount);
    }

    //not to self this might result in a bug if it happens to soon with another one where it becomes negative
    private void removeFromStorage(WorkerBonusTypes type, int amount)
    {
        Debug.Log("Before Removal: " + data.resourceStorage[type]);
        data.resourceStorage[type] -= amount;
        updateResourceUI?.Invoke(type, data.resourceStorage[type]);
        Debug.Log("Removed " + amount + " " + type);
        Debug.Log("After Removal: " + data.resourceStorage[type]);
    }

    private bool checkCost(List<ResourceCost> item_cost)
    {

        return item_cost.TrueForAll(resource => data.resourceStorage.TryGetValue(resource.type, out int resource_amount) && resource.cost <= resource_amount);
    }

    public void importResourceData(ResourceData data)
    {
        this.data = data;
    }
}
