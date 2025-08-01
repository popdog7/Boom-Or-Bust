using System.Collections.Generic;
using UnityEngine;
using static ResourceIconsSO;

[CreateAssetMenu(fileName = "jobsiteUnlockables", menuName = "Job/Jobsite Unlockables")]
public class JobsiteUnlockablesSO : ScriptableObject
{
    [System.Serializable]
    public class Unlockables
    {
        public WorkerBonusTypes type;  
        public GameObject jobsite;
    }

    public List<Unlockables> unlockable_jobsites = new List<Unlockables>();

    private Dictionary<WorkerBonusTypes, GameObject> unlockable_lookup;

    private void OnEnable()
    {
        unlockable_lookup = new Dictionary<WorkerBonusTypes, GameObject>();
        foreach (var unlockable in unlockable_jobsites)
        {
            unlockable_lookup[unlockable.type] = unlockable.jobsite;
        }
    }

    public Dictionary<WorkerBonusTypes, GameObject> getUnlockables()
    {
        return new Dictionary<WorkerBonusTypes, GameObject>(unlockable_lookup);
    }
}
