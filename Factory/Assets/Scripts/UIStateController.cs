using System;
using System.Collections.Generic;
using UnityEngine;

public class UIStateController : MonoBehaviour
{
    [SerializeField] private List<CanvasGroup> ui_containers = new List<CanvasGroup>();

    private void Start()
    {
        changeUIGroup(0);
    }

    public void changeUIGroup(int index)
    {
        
        for (int i = 0; i < ui_containers.Count; i++)
        {
            bool is_active_page = (i == index);

            ui_containers[i].alpha = is_active_page ? 1.0f : 0.0f;
            ui_containers[i].interactable = is_active_page;
            ui_containers[i].blocksRaycasts = is_active_page;
        }
        
    }
}
