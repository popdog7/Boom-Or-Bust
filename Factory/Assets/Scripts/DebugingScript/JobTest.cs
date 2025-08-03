using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class JobTest : MonoBehaviour
{
    [SerializeField] private ResourceManager resourceManager;

    [SerializeField] private List<JobSite> sites;

    [SerializeField] private List<Worker> workers;

    [SerializeField] private bool assignment_debug = false;
    [SerializeField] private bool production_debug = false;
    [SerializeField] private bool connection_debug = false;

    void Start()
    {
        if (assignment_debug)
        {
            assignmentTest();
        }
        if (production_debug)
        {
            productionTest();
        }
        if (connection_debug)
        {
            connectionTest();
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

    private void assignmentTest()
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

    private void productionTest()
    {
        resourceManager.connectWorker(workers[0]);
        sites[0].hireWorker(workers[0]);
        resourceManager.connectWorker(workers[1]);
        sites[1].hireWorker(workers[1]);
        resourceManager.connectWorker(workers[2]);
        sites[2].hireWorker(workers[2]);
    }

    private void connectionTest()
    {
        resourceManager.connectWorker(workers[0]);
        resourceManager.connectWorker(workers[1]);
        resourceManager.connectWorker(workers[2]);
    }

    public void buttonTest()
    {
        Debug.Log("I Pressed The button");
    }
}

