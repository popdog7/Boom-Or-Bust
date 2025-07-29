using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] private WorkerStats worker_stats;

    public JobSite job { get; private set; }

    private float task_timer;
    private Coroutine produce_coroutine;

    void Start()
    {
        Dictionary<WorkerBonusTypes, float> bonus_lookup = new Dictionary<WorkerBonusTypes, float>(worker_stats.GetAllBonuses());

        /*
        foreach (var bonus in bonus_lookup)
        {
            Debug.Log(bonus.Key + " " + bonus.Value);
        }
        */
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

    #region pause functions

    public void pauseProdcution()
    {
        if (produce_coroutine != null)
        {
            StopCoroutine(produce_coroutine);
        }
    }

    public void unpauseProdcution()
    {
        if (produce_coroutine == null)
        {
            produce_coroutine = StartCoroutine(ProduceLoop());
        }
    }

    #endregion

    private IEnumerator ProduceLoop()
    {
        while (job != null)
        {
            yield return new WaitForSeconds(task_timer);
        }
    }

    public void returnToJobsite()
    {

    }

    public void trainEmployee()
    {

    }
}
