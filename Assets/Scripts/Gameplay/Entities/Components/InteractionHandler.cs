using System;
using System.Collections.Generic;
using UnityEngine;
using SurvivalGame.Gameplay.Helpers;
using SurvivalGame.Gameplay.Interactions;

namespace SurvivalGame.Gameplay.Entities.Components
{
    public class InteractionHandler : MonoBehaviour
    {
        [Tooltip("Collider to search for interactables.")]
        [SerializeField] private TriggerCallbackDispatcher interactionRange;

        public event Action<ClosestInteractableChangedEventArgs> ClosestInteractableChanged;

        private GameObject previousClosestInteractable;
        private GameObject closestInteractable;
        private List<GameObject> interactablesInRange = new();

        private void OnEnable()
        {
            interactionRange.TriggerEntered += OnInteractionTriggerEntered;
            interactionRange.TriggerExited += OnInteractionTriggerExited;
        }

        private void OnDisable()
        {
            interactionRange.TriggerEntered -= OnInteractionTriggerEntered;
            interactionRange.TriggerExited -= OnInteractionTriggerExited;
        }

        private void Update()
        {
            UpdateClosestInteractable();
        }

        private void UpdateClosestInteractable()
        {
            closestInteractable = null;
            float closestDistance = float.MaxValue;
            
            for (int i = 0; i < interactablesInRange.Count; i++)
            {
                GameObject interactable = interactablesInRange[i];

                float sqrDistance = Vector3.SqrMagnitude(transform.position - interactable.transform.position);
                
                if (sqrDistance < closestDistance)
                {
                    closestDistance = sqrDistance;
                    closestInteractable = interactable;

                }
            }
            
            if (closestInteractable != previousClosestInteractable)
            {
                ClosestInteractableChanged?.Invoke(new ClosestInteractableChangedEventArgs(closestInteractable));
            }
            
            previousClosestInteractable = closestInteractable;
        }

        private void OnInteractionTriggerEntered(Collider other)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactablesInRange.Add(other.gameObject);
            }
        }

        private void OnInteractionTriggerExited(Collider other)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactablesInRange.Remove(other.gameObject);
            }
        }

        public void TryInteractWithClosestInteractable()
        {
            closestInteractable?.GetComponent<IInteractable>().Interact();
        }
    }

    public class ClosestInteractableChangedEventArgs
    {
        public readonly GameObject NewClosestInteractable;

        public ClosestInteractableChangedEventArgs(GameObject newClosestInteractable)
        {
            NewClosestInteractable = newClosestInteractable;
        }
    }
}
