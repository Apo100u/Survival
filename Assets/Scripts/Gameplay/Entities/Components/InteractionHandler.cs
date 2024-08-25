using UnityEngine;

namespace SurvivalGame.Gameplay.Entities.Components
{
    public class InteractionHandler : MonoBehaviour
    {
        [Header("Collider to search for interactables.")]
        [SerializeField] private Collider interactionsArea;
        
        public void TryInteractWithClosestInteractable()
        {
            
        }
    }
}
