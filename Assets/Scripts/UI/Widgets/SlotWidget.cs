using System;
using TMPro;
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
        [SerializeField] private GameObject tooltipParent;
        [SerializeField] private TMP_Text tooltipText;

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

        public void SetTooltip(string text)
        {
            tooltipText.text = text;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            background.color = highlightedBackgroundColor;

            if (!string.IsNullOrEmpty(tooltipText.text))
            {
                tooltipParent.SetActive(true);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            background.color = defaultBackgroundColor;
            tooltipParent.gameObject.SetActive(false);
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
