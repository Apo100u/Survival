using System;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalGame.UI.Widgets
{
    public class PlayerCraftingWidget : SlidingWidget
    {
        [Header("Player Crafting Widget Dependencies")]
        [SerializeField] private Button backButton;
        
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
