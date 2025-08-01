using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_JobSite", menuName = "Job/JobStats")]
public class JobSiteStatsSO : ScriptableObject
{
    public string jobsite_name;
    public WorkerBonusTypes type;
    public ItemCostSO itemCost;

    public string description = "";
    public int purchase_cost = 0;

    public List<WorkerBonusTypes> prerequisite = new List<WorkerBonusTypes>();

    public int amount_produced = 1;
    public int max_workers = 1;

    public float time_to_produce = 1f;

}
