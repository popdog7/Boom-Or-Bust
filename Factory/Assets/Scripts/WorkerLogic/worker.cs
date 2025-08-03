using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Worker : MonoBehaviour
{
    [SerializeField] private WorkerStats worker_stats;
    [SerializeField] private Image progress_bar;
    [SerializeField] private CanvasGroup progress_bar_group;

    public event Action<WorkerBonusTypes, int, ItemCostSO> onCreateProduct;
    public JobSite job { get; private set; }

    Dictionary<WorkerBonusTypes, float> bonus_lookup;
    private float task_timer;
    private Coroutine produce_coroutine;

    public void initalize(WorkerStats stats)
    {
        worker_stats = stats;
        bonus_lookup = new Dictionary<WorkerBonusTypes, float>(worker_stats.GetAllBonuses());
    }

    #region Job Setting
    public void unassignJobsite()
    {
        pauseProdcution();
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
            produce_coroutine = null;
            progress_bar.fillAmount = 0f;
            progress_bar_group.alpha = 0;
        }
    }

    public void unpauseProdcution()
    {
        if (produce_coroutine == null && job != null)
        {
            progress_bar.fillAmount = 0f;
            progress_bar_group.alpha = 1;
            produce_coroutine = StartCoroutine(ProduceLoop());
        }
    }

    #endregion

    private IEnumerator ProduceLoop()
    {
        while (job != null)
        {
            float elasped_time = 0f;

            while (elasped_time < task_timer)
            {
                elasped_time += Time.deltaTime;
                progress_bar.fillAmount = elasped_time / task_timer;
                yield return null;
            }
            onCreateProduct?.Invoke(job.getJobStats().type, job.getJobStats().amount_produced, job.getJobStats().itemCost);
        }
    }

    public void returnToJobsite()
    {

    }

    public void trainEmployee()
    {
        task_timer = job.getJobStats().time_to_produce;
        unpauseProdcution();
    }
}
