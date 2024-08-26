using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SurvivalGame.UI.Widgets
{
    public class SlotWidget : Widget, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Dependencies")]
        [SerializeField] private Image background;
        [SerializeField] private Image displayImage;

        [Header("Dependencies")]
        [SerializeField] private Color defaultBackgroundColor = Color.white;
        [SerializeField] private Color highlightedBackgroundColor  = Color.white;
        
        public void SetDisplayImage(Sprite sprite)
        {
            displayImage.gameObject.SetActive(sprite);
            
            if (sprite)
            {
                displayImage.sprite = sprite;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            background.color = highlightedBackgroundColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            background.color = defaultBackgroundColor;
        }
    }
}
