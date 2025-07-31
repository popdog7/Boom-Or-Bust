using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabMenu : MonoBehaviour
{
    [SerializeField, Range(0,3)] private int index = 0;

    [SerializeField] private ToggleGroup toggle_group;
    [SerializeField] private List<Toggle> tabs = new List<Toggle>();
    [SerializeField] private List<CanvasGroup> pages = new List<CanvasGroup>();

    private void Awake()
    {
        openPage(index);
        foreach (var toggle in tabs)
        {
            toggle.onValueChanged.AddListener(checkForTab);
            toggle.group = toggle_group;
        }
    }

    private void OnDestroy()
    {
        foreach (var toggle in tabs)
        {
            toggle.onValueChanged.RemoveListener(checkForTab);
        }
    }

    private void checkForTab(bool value)
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            if (!tabs[i].isOn) continue;
            index = i;
        }
        openPage(index);
    }

    private void openPage(int index)
    {
        for (int i = 0; i < pages.Count; i++)
        {
            bool is_active_page = (i == index);

            pages[i].alpha = is_active_page ? 1.0f : 0.0f;
            pages[i].interactable = is_active_page;
            pages[i].blocksRaycasts = is_active_page;
        }

    }

    public void jumpToPage(int page)
    {
        tabs[index].isOn = true;
    }

}
