using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class ResearchPurchaseController : MonoBehaviour
{
    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI research_name;
    [SerializeField] private TextMeshProUGUI Description;
    [SerializeField] private TextMeshProUGUI input_cost;
    [SerializeField] private TextMeshProUGUI output_cost;
    [SerializeField] private TextMeshProUGUI unlock_cost;

    [Header("UI elements")]
    [SerializeField] private List<ResearchUiElement> ui_elements = new List<ResearchUiElement>();
    private ResearchUiElement active_ui;

    [Header("Jobsites")]
    [SerializeField] private List<GameObject> jobsites_objs = new List<GameObject>();
    [SerializeField] private List<GameObject> jobs_to_unlock = new List<GameObject>();

    private Dictionary<WorkerBonusTypes, GameObject> jobsites = new Dictionary<WorkerBonusTypes, GameObject>();

    public event Action<int> onResearchUnlockAttempt;

    private bool can_purchase = false;

    private void Awake()
    {
        foreach(var ui in ui_elements)
        {
            ui.onUISelected += elementSelected;
        }

    }

    private void Start()
    {
        int index = 0;
        foreach (WorkerBonusTypes type in Enum.GetValues(typeof(WorkerBonusTypes)))
        {
            jobsites[type] = jobsites_objs[index];
            index++;
        }

        setBlank();
        activateUnlockedJobs();
    }

    private void setBlank()
    {
        research_name.text = "";
        Description.text = "";
        input_cost.text = "";
        output_cost.text = "";
        unlock_cost.text = "Select Icon";
    }

    private void elementSelected(ResearchUiElement ui)
    {
        active_ui = ui;
        JobSiteStatsSO job_stats = ui.getStats();

        research_name.text = job_stats.jobsite_name;
        Description.text = job_stats.description;

        input_cost.text = formatOutputCost(job_stats.itemCost.item_cost);


        if (ui.getUnlockStatus())
        {
            unlock_cost.text = "Purchased";
            can_purchase = false;
        }
        else
        {
            unlock_cost.text = "$" + job_stats.purchase_cost.ToString();
            can_purchase = true;
        }
    }

    private string formatOutputCost(List<ItemCostSO.ResourceCost> cost)
    {
        
        string costText = "";

        if (cost.Count == 0)
            return "No Input";

        for (int i = 0; i < cost.Count; i++)
        {
            var rc = cost[i];
            costText += $"{rc.type}: {rc.cost}";

            if (i < cost.Count - 1)
                costText += "\n";
        }
        
        return costText;
    }

    private void unlockElement()
    {
        active_ui.unlock();
        unlock_cost.text = "Purchased";
    }

    public void unlockResearch()
    {
        if (active_ui == null || !can_purchase)
            return;

        JobSiteStatsSO job_stats = active_ui.getStats();
        
        if(!checkPrerequisites(job_stats.prerequisite))
            return;

        can_purchase = false;
        onResearchUnlockAttempt?.Invoke(job_stats.purchase_cost);
    }

    public void researchUnlockOutcome(bool outcome)
    {
        if (outcome)
        {
            WorkerBonusTypes job_type = active_ui.getStats().type;

            unlockElement();
            jobs_to_unlock.Add(jobsites[job_type]);
        }
        else
        {

        }
    }

    public void activateUnlockedJobs()
    {
        foreach (var job in jobs_to_unlock)
        {
            job.SetActive(true);
        }
        jobs_to_unlock = new List<GameObject>();
    }

    private bool checkPrerequisites(List<WorkerBonusTypes> prereqs)
    {
        foreach (var req in prereqs)
        {
            if (!jobsites[req].activeSelf)
                return false;
        }

        return true;
    }
}
