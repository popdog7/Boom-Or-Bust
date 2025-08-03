using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(JobsiteUnlockablesSO))]
public class JobsiteUnlockablesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        JobsiteUnlockablesSO unlockables = (JobsiteUnlockablesSO)target;


        if (GUILayout.Button("Populate Resource Bonuses"))
        {
            populateSO(unlockables);
            EditorUtility.SetDirty(unlockables);
        }

        if (GUILayout.Button("Clear Invalid Bonuses"))
        {
            clearSO(unlockables);
            EditorUtility.SetDirty(unlockables);
        }

        if (GUILayout.Button("Sync Bonuses"))
        {
            clearSO(unlockables);
            populateSO(unlockables);
            EditorUtility.SetDirty(unlockables);
        }
    }

    private void populateSO(JobsiteUnlockablesSO _unlockables)
    {
        foreach (WorkerBonusTypes type in Enum.GetValues(typeof(WorkerBonusTypes)))
        {
            if (!_unlockables.unlockable_jobsites.Exists(entry => entry.type == type))
            {
                _unlockables.unlockable_jobsites.Add(new JobsiteUnlockablesSO.Unlockables
                {
                    type = type,
                    jobsite = null
                });
            }
        }

        _unlockables.unlockable_jobsites.Sort((a, b) => a.type.CompareTo(b.type));
    }

    private void clearSO(JobsiteUnlockablesSO _unlockables)
    {
        _unlockables.unlockable_jobsites.RemoveAll(entry => !System.Enum.IsDefined(typeof(WorkerBonusTypes), entry.type));
    }
}
