using UnityEngine;

namespace SurvivalGame.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Resource Data", menuName = "Survival Game/Resource Data")]
    public class ResourceData : ScriptableObject
    {
        [Tooltip("GameObject of the resource that appears in the game world.")]
        [SerializeField] private GameObject prefab;
        
        [Tooltip("Image of the resource shown in UI.")]
        [SerializeField] private Sprite image;
    }
}
