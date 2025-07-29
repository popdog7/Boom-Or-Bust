using UnityEngine;

[CreateAssetMenu(fileName = "JobSiteStatsSO", menuName = "Scriptable Objects/JobSiteStatsSO")]
public class JobSiteStatsSO : ScriptableObject
{
    public int amount_produced = 1;
    public int max_workers = 1;

    public float time_to_produce = 1f;

}
