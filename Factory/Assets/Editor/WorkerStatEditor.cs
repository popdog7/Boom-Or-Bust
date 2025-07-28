using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WorkerStats))]
public class WorkerStatEditor: Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WorkerStats stats = (WorkerStats)target;


        if(GUILayout.Button("Populate Resource Bonuses"))
        {
            populateSO(stats);
            EditorUtility.SetDirty(stats);
        }

        if (GUILayout.Button("Clear Invalid Bonuses"))
        {
            clearSO(stats);
            EditorUtility.SetDirty(stats);
        }

        if (GUILayout.Button("Sync Bonuses"))
        {
            clearSO(stats);
            populateSO(stats);
            EditorUtility.SetDirty(stats);
        }
    }

    private void populateSO(WorkerStats _stats)
    {
        foreach (WorkerBonusTypes type in Enum.GetValues(typeof(WorkerBonusTypes)))
        {
            if (!_stats.resource_bonuses.Exists(entry => entry.type == type))
            {
                _stats.resource_bonuses.Add(new WorkerStats.ResourceBonus
                {
                    type = type,
                    bonus_amount = 1f
                });
            }
        }

        _stats.resource_bonuses.Sort((a, b) => a.type.CompareTo(b.type));
    }

    private void clearSO(WorkerStats _stats)
    {
        _stats.resource_bonuses.RemoveAll(entry => !System.Enum.IsDefined(typeof(WorkerBonusTypes), entry.type));
    }
}
