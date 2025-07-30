using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ResourceIconsSO))]
public class ResourceIconEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ResourceIconsSO icons = (ResourceIconsSO)target;


        if (GUILayout.Button("Populate Resource Bonuses"))
        {
            populateSO(icons);
            EditorUtility.SetDirty(icons);
        }

        if (GUILayout.Button("Clear Invalid Bonuses"))
        {
            clearSO(icons);
            EditorUtility.SetDirty(icons);
        }

        if (GUILayout.Button("Sync Bonuses"))
        {
            clearSO(icons);
            populateSO(icons);
            EditorUtility.SetDirty(icons);
        }
    }

    private void populateSO(ResourceIconsSO _icons)
    {
        foreach (WorkerBonusTypes type in Enum.GetValues(typeof(WorkerBonusTypes)))
        {
            if (!_icons.resource_icons.Exists(entry => entry.type == type))
            {
                _icons.resource_icons.Add(new ResourceIconsSO.ResourceIcons
                {
                    type = type,
                    icon = null
                });
            }
        }

        _icons.resource_icons.Sort((a, b) => a.type.CompareTo(b.type));
    }

    private void clearSO(ResourceIconsSO _icons)
    {
        _icons.resource_icons.RemoveAll(entry => !System.Enum.IsDefined(typeof(WorkerBonusTypes), entry.type));
    }
}
