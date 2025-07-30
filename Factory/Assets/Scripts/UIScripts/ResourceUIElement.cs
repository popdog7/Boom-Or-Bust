using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourceUIElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI element_text;
    [SerializeField] private Image element_icon;

    public void initalize(Sprite icon)
    {
        element_text.text = "0";
        element_icon.sprite = icon;
    }

    public void updateUI(int amount)
    {
        element_text.text = amount.ToString();
    }
}
