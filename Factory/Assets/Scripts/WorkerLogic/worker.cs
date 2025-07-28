using System.Collections.Generic;
using UnityEngine;

public class worker : MonoBehaviour
{
    [SerializeField] private WorkerStats worker_stats;

    void Start()
    {
        Dictionary<WorkerBonusTypes, float> bonus_lookup = new Dictionary<WorkerBonusTypes, float>(worker_stats.GetAllBonuses());

        foreach (var bonus in bonus_lookup)
        {
            Debug.Log(bonus.Key + " " + bonus.Value);
        }
    }
}
