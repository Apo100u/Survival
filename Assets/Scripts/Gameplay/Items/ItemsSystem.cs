using System;
using SurvivalGame.CustomDataStructures.OutputTree;
using SurvivalGame.ScriptableObjects;
using UnityEngine;

namespace SurvivalGame.Gameplay.Items
{
    public class ItemsSystem : MonoBehaviour
    {
        [Header("Game Data")]
        [Tooltip("Assign all items that are available in the game.")]
        [SerializeField] private ItemData[] items;
        [Tooltip("Assign all recipes that are available in the game.")]
        [SerializeField] private RecipeData[] recipes;

        private OutputTree<int> recipeTreeByUniqueIds;
        
        public void Init()
        {
            AssignUniqueIdsToItems();
            CreateRecipeTree();
        }

        private void AssignUniqueIdsToItems()
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].AssignUniqueId(i);
            }
        }

        private void CreateRecipeTree()
        {
            recipeTreeByUniqueIds = new OutputTree<int>();

            for (int i = 0; i < recipes.Length; i++)
            {
                int[] ingredientsUniqueIds = recipes[i].GetIngredientsUniqueIds();
                Array.Sort(ingredientsUniqueIds);

                recipeTreeByUniqueIds.AddBranch(ingredientsUniqueIds, recipes[i].SuccessfulOutput.UniqueId);
            }
        }
    }
}
