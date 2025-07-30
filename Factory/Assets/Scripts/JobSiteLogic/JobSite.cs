using System.Collections.Generic;
using UnityEngine;

public class JobSite : MonoBehaviour
{
    [SerializeField] private JobSiteStatsSO job_stats;

    private List<Worker> employees = new List<Worker>();

    public void hireWorker(Worker potential_employee)
    {
        if(employees.Count < job_stats.max_workers)
        {
            //if the workers has a job it needs to unassign itself from that job
            //assign this job to it
            //cause employee to recalculate all job related math

            if (potential_employee.job != null)
            {
                potential_employee.job.fireWorker(potential_employee);
            }


            potential_employee.assignJobsite(this);
            employees.Add(potential_employee);
            potential_employee.trainEmployee();
        }

        //if not we send the worker back to the position of their workstation
        potential_employee.returnToJobsite();
    }

    public void fireWorker(Worker employee)
    {
        //worker needs to unassign itself from job
        employees.Remove(employee);
        employee.unassignJobsite();   
    }

    public void layoffWorkForce()
    {
        foreach(var employee in new List<Worker>(employees))
        {
            fireWorker(employee);
        }

        employees.Clear();
    }

    public string getJobsiteName()
    {
        return job_stats.jobsite_name;
    }

    public JobSiteStatsSO getJobStats()
    {
        return job_stats;
    }
}
