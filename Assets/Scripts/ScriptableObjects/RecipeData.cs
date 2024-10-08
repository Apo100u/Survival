using UnityEngine;

namespace SurvivalGame.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Recipe Data", menuName = "Survival Game/Recipe Data")]
    public class RecipeData : UniqueIdScriptableObject
    {
        [Tooltip("Items needed to for this recipe in any order.")]
        [SerializeField] private ItemData[] ingredients;

        [Tooltip("Item received when crafting is successful.")]
        [SerializeField] private ItemData successfulOutput;

        [Tooltip("Chance of receiving successful output. Range 0 - 1 (0 is 0%, 1 is 100%).")]
        [SerializeField, Range(0f, 1f)] private float successChance = 0.5f;

        [Tooltip("How much time in seconds it will take to craft the output.")]
        [SerializeField] private float craftingTimeInSeconds = 3.0f;

        public ItemData[] Ingredients => ingredients;
        public ItemData SuccessfulOutput => successfulOutput;
        public float SuccessChance => successChance;
        public float CraftingTimeInSeconds => craftingTimeInSeconds;
        
        public int[] GetIngredientsUniqueIds()
        {
            int[] uniqueIds = new int[ingredients.Length];

            for (int i = 0; i < ingredients.Length; i++)
            {
                uniqueIds[i] = ingredients[i].UniqueId;
            }

            return uniqueIds;
        }
    }
}
