using System;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalGame.UI.Widgets
{
    public class PlayerInventoryWidget : InventoryWidget
    {
        [Header("Player Inventory Widget Dependencies")]
        [SerializeField] private Button craftingButton;

        public event Action CraftingButtonInteracted;

        public override void Init(Camera camera, RectTransform area)
        {
            base.Init(camera, area);

            craftingButton.onClick.AddListener(OnCraftingButtonInteracted);
        }
        
        private void OnCraftingButtonInteracted()
        {
            CraftingButtonInteracted?.Invoke();
        }
    }
}
