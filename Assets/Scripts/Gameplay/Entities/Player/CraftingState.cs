using SurvivalGame.Gameplay.Helpers.Calculators;
using SurvivalGame.Gameplay.Items;
using SurvivalGame.UI.Widgets;
using UnityEngine;

namespace SurvivalGame.Gameplay.Entities.Player
{
    public class CraftingState : PlayerState
    {
        private ItemsSystem itemsSystem;
        private CraftingCalculator craftingCalculator;
        
        public CraftingState(PlayerDependencies playerDependencies, Transform cameraTarget, ItemsSystem itemsSystem) : base(playerDependencies, cameraTarget)
        {
            this.itemsSystem = itemsSystem;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            craftingCalculator = new CraftingCalculator(itemsSystem.RecipeTreeByUniqueIds);
            ShowCraftingUI(true);
            SetupLookAtVisualsForCrafting();
            
            hud.PlayerCraftingWidget.IngredientAdded += OnIngredientAdded;
            hud.PlayerCraftingWidget.IngredientRemoved += OnIngredientRemoved;
        }

        public override void OnExit()
        {
            base.OnExit();
            
            craftingCalculator = null;
            ShowCraftingUI(false);
            
            hud.PlayerCraftingWidget.IngredientAdded -= OnIngredientAdded;
            hud.PlayerCraftingWidget.IngredientRemoved -= OnIngredientRemoved;
        }
        
        private void OnIngredientAdded(IngredientEventArgs args)
        {
            craftingCalculator.AddIngredient(args.Ingredient);
        }

        private void OnIngredientRemoved(IngredientEventArgs args)
        {
            craftingCalculator.RemoveIngredient(args.Ingredient);
        }

        private void ShowCraftingUI(bool show)
        {
            hud.PlayerInventoryWidget.Show(!show);
            hud.PlayerCraftingWidget.Show(show);

            if (show)
            {
                hud.PlayerCraftingWidget.ShowItems(inventory.GetItems());
            }
        }

        private void SetupLookAtVisualsForCrafting()
        {
            Vector3 lootAtTarget = visuals.transform.position - Vector3.forward;
            visuals.LookAt(lootAtTarget);
        }
    }
}
