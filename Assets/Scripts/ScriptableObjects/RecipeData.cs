using UnityEngine;

namespace SurvivalGame.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Recipe Data", menuName = "Survival Game/Recipe Data")]
    public class RecipeData : ScriptableObject
    {
        [Tooltip("Resources needed to for this recipe in any order.")]
        [SerializeField] private ResourceData[] ingredients;
        
        [Tooltip("Resource received when crafting is successful.")]
        [SerializeField] private ResourceData successfulOutput;

        [Tooltip("Chance of receiving successful output. Range 0 - 1 (0 is 0%, 1 is 100%).")]
        [SerializeField, Range(0f, 1f)] private float successChance = 0.5f;
    }
}
