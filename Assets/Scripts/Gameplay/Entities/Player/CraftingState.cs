using SurvivalGame.Gameplay.Helpers.Calculators;
using SurvivalGame.Gameplay.Items;
using SurvivalGame.ScriptableObjects;
using SurvivalGame.UI.Widgets;
using UnityEngine;

namespace SurvivalGame.Gameplay.Entities.Player
{
    public class CraftingState : PlayerState
    {
        private ItemsSystem itemsSystem;
        private CraftingCalculator craftingCalculator;
        private RecipeData recipeFromCurrentIngredients;
        
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
            
            hud.PlayerCraftingWidget.CraftButtonInteracted += OnCraftButtonInteracted;
            hud.PlayerCraftingWidget.IngredientAdded += OnIngredientAdded;
            hud.PlayerCraftingWidget.IngredientRemoved += OnIngredientRemoved;
        }

        public override void OnExit()
        {
            base.OnExit();
            
            craftingCalculator = null;
            ShowCraftingUI(false);
            
            hud.PlayerCraftingWidget.CraftButtonInteracted -= OnCraftButtonInteracted;
            hud.PlayerCraftingWidget.IngredientAdded -= OnIngredientAdded;
            hud.PlayerCraftingWidget.IngredientRemoved -= OnIngredientRemoved;
        }
        
        
        private void OnCraftButtonInteracted()
        {
            if (recipeFromCurrentIngredients)
            {
                TryCrafting();
            }
        }

        private void TryCrafting()
        {
            hud.PlayerCraftingWidget.PlayCraftingAnimation(recipeFromCurrentIngredients.CraftingTimeInSeconds, OnCraftingAnimationEnded);
        }

        private void OnCraftingAnimationEnded()
        {
            bool isCraftingSuccessful = Random.Range(0f, 1f) <= recipeFromCurrentIngredients.SuccessChance;

            for (int i = 0; i < recipeFromCurrentIngredients.Ingredients.Length; i++)
            {
                inventory.RemoveItem(recipeFromCurrentIngredients.Ingredients[i]);
                hud.PlayerCraftingWidget.ClearIngredientSlots();
            }
        }

        private void OnIngredientAdded(IngredientEventArgs args)
        {
            craftingCalculator.AddIngredient(args.Ingredient);
            UpdateCurrentRecipe();
        }

        private void OnIngredientRemoved(IngredientEventArgs args)
        {
            craftingCalculator.RemoveIngredient(args.Ingredient);
            UpdateCurrentRecipe();
        }

        private void UpdateCurrentRecipe()
        {
            int? recipeUniqueId = craftingCalculator.GetOutputRecipeUniqueId();

            recipeFromCurrentIngredients = recipeUniqueId == null
                ? null
                : itemsSystem.GetRecipeByUniqueId((int)recipeUniqueId);

            UpdateRecipeOutputOnCraftingWidget();
        }

        private void UpdateRecipeOutputOnCraftingWidget()
        {
            hud.PlayerCraftingWidget.UpdateOutputSlot(recipeFromCurrentIngredients);
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
