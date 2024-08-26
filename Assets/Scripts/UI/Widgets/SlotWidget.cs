using UnityEngine;
using UnityEngine.UI;

namespace SurvivalGame.UI.Widgets
{
    public class SlotWidget : Widget
    {
        [SerializeField] private Image displayImage;

        public void SetDisplayImage(Sprite sprite)
        {
            displayImage.gameObject.SetActive(sprite);
            
            if (sprite)
            {
                displayImage.sprite = sprite;
            }
        }
    }
}
