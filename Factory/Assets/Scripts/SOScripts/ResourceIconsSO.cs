using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ResourceIcons", menuName = "Icons/Resource Icons")]
public class ResourceIconsSO : ScriptableObject
{
    [System.Serializable]
    public class ResourceIcons
    {
        public WorkerBonusTypes type;
        public Sprite icon;
    }

    public List<ResourceIcons> resource_icons = new List<ResourceIcons>();

    private Dictionary<WorkerBonusTypes, Sprite> icon_lookup;

    private void OnEnable()
    {
        icon_lookup = new Dictionary<WorkerBonusTypes, Sprite>();
        foreach (var bonus in resource_icons)
        {
            icon_lookup[bonus.type] = bonus.icon;
        }
    }

    public Dictionary<WorkerBonusTypes, Sprite> getIcons()
    {
        return new Dictionary<WorkerBonusTypes, Sprite>(icon_lookup);
    }
}
