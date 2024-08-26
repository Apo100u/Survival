using System;
using System.Text;
using UnityEngine;
using SurvivalGame.Gameplay.Entities.Components;
using SurvivalGame.Gameplay.Helpers;
using SurvivalGame.Gameplay.Interactions;
using SurvivalGame.Gameplay.Items;
using SurvivalGame.UI;
using Input = SurvivalGame.Gameplay.Entities.Components.Input;

namespace SurvivalGame.Gameplay.Entities
{
    [RequireComponent(typeof(Input), typeof(Movement), typeof(Inventory))]
    [RequireComponent(typeof(InteractionHandler))]
    public class Player : Entity
    {
        [Header("Player Dependencies")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private PlayerHUD hud;
        
        private Input input;
        private Movement movement;
        private Inventory inventory;
        private InteractionHandler interactionHandler;
        
        public override void Init(ObjectPools objectPools)
        {
            AssignComponents();
            hud.Init(mainCamera);
        }

        private void AssignComponents()
        {
            input              = GetComponent<Input>();
            movement           = GetComponent<Movement>();
            inventory          = GetComponent<Inventory>();
            interactionHandler = GetComponent<InteractionHandler>();
        }

        private void OnEnable()
        {
            if (!interactionHandler)
            {
                interactionHandler = GetComponent<InteractionHandler>();
            }
            
            interactionHandler.InteractionExecuted += OnInteractionExecuted;
            interactionHandler.ClosestInteractableChanged += OnClosestInteractableChanged;
        }

        private void OnDisable()
        {
            interactionHandler.InteractionExecuted -= OnInteractionExecuted;
            interactionHandler.ClosestInteractableChanged -= OnClosestInteractableChanged;
        }

        private void Update()
        {
            ProcessInputActions();
            UpdateVisualsLookAt(input.GetAimWorldPosition(mainCamera));
        }

        private void ProcessInputActions()
        {
            movement.Move(input.GetNormalizedMovementInput() * Time.deltaTime);

            if (input.GetInteractionDown())
            {
                interactionHandler.TryInteractWithClosestInteractable();
            }

            if (input.GetInventoryDown())
            {
                hud.PlayerInventoryWidget.ToggleShow();
                UpdateInventoryWidget();
            }
        }
        
        private void OnInteractionExecuted(InteractionExecutedEventArgs args)
        {
            Item item = args.Interactable.GetComponent<Item>();

            if (item)
            {
                inventory.AddItem(item.ItemData);

                if (hud.PlayerInventoryWidget.IsShown)
                {
                    UpdateInventoryWidget();
                }
                
                objectPools.ReturnToPool(item.ItemData.Prefab, item.gameObject);
            }
        }

        private void OnClosestInteractableChanged(ClosestInteractableChangedEventArgs args)
        {
            hud.TooltipWidget.Show(args.NewClosestInteractable);
            
            if (args.NewClosestInteractable)
            {
                UpdateInteractableTooltip(args.NewClosestInteractable);
            }
        }

        private void UpdateInteractableTooltip(GameObject interactable)
        {
            Item item = interactable.GetComponent<Item>();
            
            StringBuilder message = new(item ? item.ItemData.DisplayName : interactable.name);
            message.Append(Environment.NewLine);
            message.Append($"[{input.InteractKey}] - ");
            message.Append(interactable.GetComponent<IInteractable>().GetInteractionMessage());

            hud.TooltipWidget.SetTransformToFollow(interactable.transform);
            hud.TooltipWidget.UpdateText(message.ToString());
        }

        private void UpdateInventoryWidget()
        {
            hud.PlayerInventoryWidget.ShowItems(inventory.GetItems());
        }
    }
}
