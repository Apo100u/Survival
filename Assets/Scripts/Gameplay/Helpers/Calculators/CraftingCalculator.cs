using System.Collections.Generic;
using SurvivalGame.CustomDataStructures.OutputTree;
using SurvivalGame.ScriptableObjects;

namespace SurvivalGame.Gameplay.Helpers.Calculators
{
    public class CraftingCalculator
    {
        private OutputTree<int> recipeTree;
        private List<int> ingredientsUniqueIds = new();

        public CraftingCalculator(OutputTree<int> recipeTree)
        {
            this.recipeTree = recipeTree;
        }

        public void AddIngredient(ItemData ingredientData)
        {
            ingredientsUniqueIds.Add(ingredientData.UniqueId);
        }

        public void RemoveIngredient(ItemData ingredientData)
        {
            ingredientsUniqueIds.Remove(ingredientData.UniqueId);
        }

        public int? GetOutputRecipeUniqueId()
        {
            ingredientsUniqueIds.Sort();
            int? outputRecipeId = recipeTree.GetOutput(ingredientsUniqueIds);

            return outputRecipeId;
        }
    }
}
