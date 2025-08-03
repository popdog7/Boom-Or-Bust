using System.Collections.Generic;
using UnityEngine;

public class ResourceData
{
    [System.Serializable]
    public class resourceAmount
    {
        public WorkerBonusTypes type;
        public int amount;
    }

    private List<resourceAmount> resourceAmounts;

    public int money = 1000;
    public int debt = 0;

    public Dictionary<WorkerBonusTypes, int> resourceStorage = new Dictionary<WorkerBonusTypes, int>();
}
