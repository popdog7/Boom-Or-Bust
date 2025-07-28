using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WorkerStats", menuName = "Worker/WorkerStats")]
public class WorkerStats : ScriptableObject
{
    [System.Serializable]
    public class ResourceBonus
    {
        public WorkerBonusTypes type;
        public float bonus_amount = 1f;
    }

    public List<ResourceBonus> resource_bonuses = new List<ResourceBonus>();

    private Dictionary<WorkerBonusTypes, float> bonus_lookup;

    private void OnEnable()
    {
        bonus_lookup = new Dictionary<WorkerBonusTypes, float>();
        foreach (var bonus in resource_bonuses)
        {
            bonus_lookup[bonus.type] = bonus.bonus_amount;
        }
    }

    public Dictionary<WorkerBonusTypes, float> GetAllBonuses()
    {
        return new Dictionary<WorkerBonusTypes, float>(bonus_lookup);
    }
}
