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

        public event Action<InteractionExecutedEventArgs> InteractionExecuted;
        public event Action<ClosestInteractableChangedEventArgs> ClosestInteractableChanged;

        private GameObject previousClosestInteractable;
        private GameObject closestInteractable;
        private List<IInteractable> interactablesInRange = new();
        private List<GameObject> interactablesGameObjectsInRange = new();

        private void OnEnable()
        {
            interactionRange.TriggerEntered += OnInteractionTriggerEntered;
            interactionRange.TriggerExited += OnInteractionTriggerExited;
            
            ClosestInteractableChanged?.Invoke(new ClosestInteractableChangedEventArgs(closestInteractable));
        }

        private void OnDisable()
        {
            interactionRange.TriggerEntered -= OnInteractionTriggerEntered;
            interactionRange.TriggerExited -= OnInteractionTriggerExited;
            
            ClosestInteractableChanged?.Invoke(new ClosestInteractableChangedEventArgs(null));
        }

        private void Update()
        {
            UnregisterInactiveInteractablesInRange();
            UpdateClosestInteractable();
        }

        private void RegisterInteractable(GameObject interactableObject)
        {
            interactablesInRange.Add(interactableObject.GetComponent<IInteractable>());
            interactablesGameObjectsInRange.Add(interactableObject);
        }

        private void UnregisterInteractable(GameObject interactableObject)
        {
            for (int i = interactablesGameObjectsInRange.Count - 1; i >= 0; i--)
            {
                if (interactablesGameObjectsInRange[i] == interactableObject)
                {
                    UnregisterInteractableAt(i);
                    break;
                }
            }
        }

        private void UnregisterInteractableAt(int index)
        {
            interactablesInRange.RemoveAt(index);
            interactablesGameObjectsInRange.RemoveAt(index);
        }

        private void UnregisterInactiveInteractablesInRange()
        {
            for (int i = interactablesGameObjectsInRange.Count - 1; i >= 0; i--)
            {
                if (!interactablesGameObjectsInRange[i].activeSelf || !interactablesInRange[i].IsInteractable())
                {
                    UnregisterInteractableAt(i);
                }
            }
        }

        private void UpdateClosestInteractable()
        {
            closestInteractable = null;
            float closestDistance = float.MaxValue;
            
            for (int i = 0; i < interactablesGameObjectsInRange.Count; i++)
            {
                GameObject interactable = interactablesGameObjectsInRange[i];

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
                RegisterInteractable(other.gameObject);
            }
        }

        private void OnInteractionTriggerExited(Collider other)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();

            if (interactable != null)
            {
                UnregisterInteractable(other.gameObject);
            }
        }

        public void TryInteractWithClosestInteractable()
        {
            IInteractable interactable = closestInteractable?.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();
                InteractionExecuted?.Invoke(new InteractionExecutedEventArgs(closestInteractable));
            }
        }
    }
    
    public class InteractionExecutedEventArgs
    {
        public readonly GameObject Interactable;

        public InteractionExecutedEventArgs(GameObject interactable)
        {
            Interactable = interactable;
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
