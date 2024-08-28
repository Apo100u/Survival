using TMPro;
using UnityEngine;

namespace SurvivalGame.UI.Widgets
{
    public class InfoWidget : SlidingWidget
    {
        [Header("Info Widget Dependencies")]
        [SerializeField] private TMP_Text infoText;

        public void SetInfoText(string text)
        {
            infoText.text = text;
        }
    }
}
