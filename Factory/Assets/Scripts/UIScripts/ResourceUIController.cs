using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceUIController : MonoBehaviour
{
    [SerializeField] private GameObject UI_element_prefab;
    [SerializeField] private Transform element_contaier;
    [SerializeField] private ResourceIconsSO icons;

    private Dictionary<WorkerBonusTypes, ResourceUIElement> element_lookup;
    private Dictionary<WorkerBonusTypes, Sprite> icon_lookup;

    private void Awake()
    {
        element_lookup = new Dictionary<WorkerBonusTypes, ResourceUIElement>();
        icon_lookup = new Dictionary<WorkerBonusTypes, Sprite>(icons.getIcons());
        createLabels();
    }

    private void createLabels()
    {
        foreach (WorkerBonusTypes type in Enum.GetValues(typeof(WorkerBonusTypes)))
        {

            GameObject labelGO = Instantiate(UI_element_prefab, element_contaier);
            ResourceUIElement element = labelGO.GetComponent<ResourceUIElement>();

            element_lookup[type] = element;
            element.initalize(icon_lookup[type]);
        }
    }

    public void updateElementUI(WorkerBonusTypes type, int amount)
    {
        element_lookup[type].updateUI(amount);
    }
}
