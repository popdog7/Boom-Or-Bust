using System;
using TMPro;
using UnityEngine;

public class ResearchUiElement : MonoBehaviour
{
    [Header("Job Info")]
    [SerializeField] private JobSiteStatsSO job_stats;

    [Header("Unlock Status")]
    [SerializeField] private bool unlock_status = false;

    public event Action<ResearchUiElement> onUISelected;

    public void elementSelected()
    {
        onUISelected?.Invoke(this);
    }

    public JobSiteStatsSO getStats()
    {
        return job_stats;
    }

    public bool getUnlockStatus()
    {
        return unlock_status;
    }

    public void unlock()
    {
        if (!unlock_status)
            unlock_status = true;
    }
}
