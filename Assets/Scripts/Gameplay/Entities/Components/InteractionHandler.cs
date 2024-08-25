using UnityEngine;
using SurvivalGame.Gameplay.Helpers;

namespace SurvivalGame.Gameplay.Entities.Components
{
    public class InteractionHandler : MonoBehaviour
    {
        [Header("Collider to search for interactables.")]
        [SerializeField] private TriggerCallbackDispatcher triggerCallbackDispatcher;
        
        

        private void OnEnable()
        {
            triggerCallbackDispatcher.TriggerEntered += OnInteractionTriggerEntered;
            triggerCallbackDispatcher.TriggerExited += OnInteractionTriggerExited;
        }

        private void OnDisable()
        {
            triggerCallbackDispatcher.TriggerEntered -= OnInteractionTriggerEntered;
            triggerCallbackDispatcher.TriggerExited -= OnInteractionTriggerExited;
        }

        private void OnInteractionTriggerEntered(Collider other)
        {
        }

        private void OnInteractionTriggerExited(Collider other)
        {
        }

        public void TryInteractWithClosestInteractable()
        {
            
        }
    }
}
