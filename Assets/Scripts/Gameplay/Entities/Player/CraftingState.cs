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
            SetupVisualsForCrafting();
            
            hud.PlayerCraftingWidget.CraftButtonInteracted += OnCraftButtonInteracted;
            hud.PlayerCraftingWidget.IngredientAdded += OnIngredientAdded;
            hud.PlayerCraftingWidget.IngredientRemoved += OnIngredientRemoved;
        }

        public override void OnExit()
        {
            base.OnExit();
            
            craftingCalculator = null;
            ShowCraftingUI(false);
            visuals.ClearAllItemsInHands();
            
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
                ItemData ingredient = recipeFromCurrentIngredients.Ingredients[i];
                visuals.RemoveItemFromHand(ingredient);
                craftingCalculator.RemoveIngredient(ingredient);
                inventory.RemoveItem(ingredient);
            }

            if (isCraftingSuccessful)
            {
                inventory.TryAddItem(recipeFromCurrentIngredients.SuccessfulOutput);
                hud.PlayerCraftingWidget.ShowItemInFirstEmptyInventorySlot(recipeFromCurrentIngredients.SuccessfulOutput);
            }

            hud.PlayerCraftingWidget.ClearIngredientSlots();
            UpdateCurrentRecipe();
            ShowCraftingFeedback(isCraftingSuccessful);
        }

        private void ShowCraftingFeedback(bool isCraftingSuccessful)
        {
            string message = isCraftingSuccessful
                ? "Crafting successful"
                : "Crafting failed";
            
            hud.InfoWidget.Show(true);
            hud.InfoWidget.SetInfoText(message);
            hud.InfoWidget.HideAfterSeconds(3.0f);
        }

        private void OnIngredientAdded(IngredientEventArgs args)
        {
            visuals.AddItemToNextFreeHand(args.Ingredient);
            craftingCalculator.AddIngredient(args.Ingredient);
            UpdateCurrentRecipe();
        }

        private void OnIngredientRemoved(IngredientEventArgs args)
        {
            visuals.RemoveItemFromHand(args.Ingredient);
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
            else
            {
                hud.PlayerInventoryWidget.ShowItems(inventory.GetItems());
            }
        }

        private void SetupVisualsForCrafting()
        {
            Vector3 lootAtTarget = visuals.transform.position - Vector3.forward;
            visuals.LookAt(lootAtTarget);
            visuals.SetWalkingAnimationActive(false);
        }
    }
}
