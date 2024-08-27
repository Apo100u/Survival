using System;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalGame.UI.Widgets
{
    public class PlayerCraftingWidget : InventoryWidget
    {
        [Header("Player Crafting Widget Dependencies")]
        [SerializeField] private Button backButton;
        [SerializeField] private Button craftButton;
        [SerializeField] private SlotWidget[] ingredientsSlotsInOrder;
        [SerializeField] private SlotWidget outputWidget;
        
        public event Action BackButtonInteracted;

        public override void Init(Camera camera, RectTransform area)
        {
            base.Init(camera, area);

            backButton.onClick.AddListener(OnBackButtonInteracted);
        }
        
        private void OnBackButtonInteracted()
        {
            BackButtonInteracted?.Invoke();
        }
    }
}
