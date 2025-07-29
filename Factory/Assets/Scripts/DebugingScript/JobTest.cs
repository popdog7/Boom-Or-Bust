using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class JobTest : MonoBehaviour
{
    [SerializeField] private List<JobSite> sites;

    [SerializeField] private List<Worker> workers;

    [SerializeField] private bool debug = false;

    void Start()
    {
        if (debug)
        {
            printWorkerJob();

            foreach (var w in workers)
            {
                sites[0].hireWorker(w);
            }

            printWorkerJob();

            foreach (var w in workers)
            {
                if (w.job == null)
                    sites[1].hireWorker(w);
            }

            printWorkerJob();

            foreach (var j in sites)
            {
                j.layoffWorkForce();
            }

            foreach (var w in workers)
            {
                sites[1].hireWorker(w);
            }

            foreach (var w in workers)
            {
                if (w.job == null)
                    sites[0].hireWorker(w);
            }

            printWorkerJob();

            foreach (var j in sites)
            {
                j.layoffWorkForce();
            }

            printWorkerJob();
        }
    }

    public void printWorkerJob()
    {
        foreach (var w in workers)
        {
            if (w.job != null)
            {
                Debug.Log("jobsite: " + w.job.getJobsiteName());

            }
            else
            {
                Debug.Log("unemployed");
            }
        }

        Debug.Log("--------------------------");
    }
}
