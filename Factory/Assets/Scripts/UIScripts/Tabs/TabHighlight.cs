using UnityEngine;
using UnityEngine.UI;

public class TabHighlight : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image focus_image;

    private void Awake()
    {
        toggle.onValueChanged.AddListener(onToggleValueChanged);
        onToggleValueChanged(toggle.isOn);
    }

    private void onToggleValueChanged(bool is_on)
    {
        if (focus_image == null)
            return;

        focus_image.color = toggle.isOn ? toggle.colors.highlightedColor : Color.clear;
    }
}
