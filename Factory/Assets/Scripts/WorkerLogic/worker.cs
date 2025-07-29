using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] private WorkerStats worker_stats;

    public JobSite job { get; private set; }

    void Start()
    {
        Dictionary<WorkerBonusTypes, float> bonus_lookup = new Dictionary<WorkerBonusTypes, float>(worker_stats.GetAllBonuses());

        foreach (var bonus in bonus_lookup)
        {
            Debug.Log(bonus.Key + " " + bonus.Value);
        }
    }


    #region Job Setting
    public void unassignJobsite()
    {
        job = null;
    }

    public void assignJobsite(JobSite _job)
    {
        job = _job;
    }
    #endregion

    public void returnToJobsite()
    {

    }

    public void trainEmployee()
    {

    }
}
