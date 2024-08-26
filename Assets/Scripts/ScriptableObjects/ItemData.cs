using UnityEngine;

namespace SurvivalGame.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Item Data", menuName = "Survival Game/Item Data")]
    public class ItemData : UniqueIdScriptableObject
    {
        [Tooltip("Human-friendly name for the resource. This is how the resource will be called in the game.")]
        [SerializeField] private string displayName;
        
        [Tooltip("GameObject of the resource that appears in the game world.")]
        [SerializeField] private GameObject prefab;
        
        [Tooltip("Image of the resource shown in UI.")]
        [SerializeField] private Sprite image;

        public string DisplayName => displayName;
        public Sprite Image => image;
    }
}
