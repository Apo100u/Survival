using System;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalGame.UI.Widgets
{
    public class PlayerInventoryWidget : InventoryWidget
    {
        [Header("Player Inventory Widget Dependencies")]
        [SerializeField] private Button craftingButton;
        
        [Header("Player Inventory Widget settings")]
        [Tooltip("How fast will the widget slide in or out when showing / hiding.")]
        [SerializeField] private float slideSpeed = 5.0f;

        public event Action CraftingButtonInteracted;
        
        private Vector2 hiddenPosition;
        private Vector2 shownPosition;
        private Vector2 targetPosition;

        public override void Init(Camera camera, RectTransform area)
        {
            base.Init(camera, area);

            craftingButton.onClick.AddListener(OnCraftingButtonInteracted);
            
            hiddenPosition = -Vector2.right * rectTransform.sizeDelta.x;
            shownPosition = Vector2.zero;
        }

        public override void Show(bool show)
        {
            if (show)
            {
                gameObject.SetActive(true);
            }

            targetPosition = show
                ? shownPosition
                : hiddenPosition;
        }

        private void Update()
        {
            if (rectTransform.anchoredPosition != targetPosition)
            {
                MoveTowardsTargetPosition();
            }
        }

        private void MoveTowardsTargetPosition()
        {
            Vector2 movedPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, targetPosition, slideSpeed * Time.deltaTime);
            rectTransform.anchoredPosition = movedPosition;

            if (movedPosition == hiddenPosition)
            {
                gameObject.SetActive(false);
            }
        }
        
        private void OnCraftingButtonInteracted()
        {
            CraftingButtonInteracted?.Invoke();
        }
    }
}
