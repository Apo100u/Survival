using System;
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
        [SerializeField] private Button interactButton;

        [Header("Settings")]
        [SerializeField] private Color defaultBackgroundColor = Color.white;
        [SerializeField] private Color highlightedBackgroundColor  = Color.white;

        public event Action<SlotInteractedEventArgs> Interacted;

        public override void Init(Camera camera, RectTransform area)
        {
            base.Init(camera, area);
            
            interactButton.onClick.AddListener(OnInteractButtonClicked);
        }

        private void OnInteractButtonClicked()
        {
            Interacted?.Invoke(new SlotInteractedEventArgs(this));
        }

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

    public class SlotInteractedEventArgs
    {
        public readonly SlotWidget SlotWidget;

        public SlotInteractedEventArgs(SlotWidget slotWidget)
        {
            SlotWidget = slotWidget;
        }
    }
}
