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

        public OutputTree<int> RecipeTreeByUniqueIds { get; private set; }
        
        public void Init()
        {
            AssignUniqueIds();
            CreateRecipeTree();
        }

        public RecipeData GetRecipeByUniqueId(int uniqueId)
        {
            return recipes[uniqueId];
        }

        private void AssignUniqueIds()
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].AssignUniqueId(i);
            }

            for (int i = 0; i < recipes.Length; i++)
            {
                recipes[i].AssignUniqueId(i);
            }
        }

        private void CreateRecipeTree()
        {
            RecipeTreeByUniqueIds = new OutputTree<int>();

            for (int i = 0; i < recipes.Length; i++)
            {
                int[] ingredientsUniqueIds = recipes[i].GetIngredientsUniqueIds();
                Array.Sort(ingredientsUniqueIds);

                RecipeTreeByUniqueIds.AddBranch(ingredientsUniqueIds, recipes[i].UniqueId);
            }
        }
    }
}
