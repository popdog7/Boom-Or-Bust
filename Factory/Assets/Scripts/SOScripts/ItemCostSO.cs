using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_Cost", menuName = "Job/JobCost")]
public class ItemCostSO : ScriptableObject
{
    [System.Serializable]
    public class ResourceCost
    {
        public WorkerBonusTypes type;
        public int cost = 1;
    }

    public List<ResourceCost> item_cost = new List<ResourceCost>();

    public bool hasCost()
    {
        if (item_cost.Count > 0)
            return true;
        return false;
    }
}
