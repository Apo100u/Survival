using TMPro;
using UnityEngine;

namespace SurvivalGame.UI.Widgets
{
    public class TooltipWidget : Widget
    {
        [SerializeField] private TextMeshProUGUI text;

        public void UpdateText(string newText)
        {
            text.text = newText;
        }
    }
}
