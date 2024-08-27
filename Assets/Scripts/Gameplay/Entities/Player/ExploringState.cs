using System;
using System.Text;
using SurvivalGame.Gameplay.Entities.Components;
using SurvivalGame.Gameplay.Interactions;
using SurvivalGame.Gameplay.Items;
using SurvivalGame.UI.Widgets;
using UnityEngine;

namespace SurvivalGame.Gameplay.Entities.Player
{
    public class ExploringState : PlayerState
    {
        public ExploringState(PlayerDependencies playerDependencies, Transform cameraTarget) : base(playerDependencies, cameraTarget)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();

            interactionHandler.InteractionExecuted += OnInteractionExecuted;
            interactionHandler.ClosestInteractableChanged += OnClosestInteractableChanged;
            hud.PlayerInventoryWidget.SlotInteracted += OnInventorySlotInteracted;
            hud.PlayerInventoryWidget.CloseButtonInteracted += OnInventoryCloseButtonInteracted;
            
            interactionHandler.enabled = true;
        }

        public override void OnExit()
        {
            base.OnExit();
            
            interactionHandler.enabled = false;
            
            interactionHandler.InteractionExecuted -= OnInteractionExecuted;
            interactionHandler.ClosestInteractableChanged -= OnClosestInteractableChanged;
            hud.PlayerInventoryWidget.SlotInteracted -= OnInventorySlotInteracted;
            hud.PlayerInventoryWidget.CloseButtonInteracted -= OnInventoryCloseButtonInteracted;
        }

        public override void Process()
        {
            base.Process();
            
            ProcessInputActions();
            visuals.LookAt(input.GetAimWorldPosition(mainCamera));
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
                ToggleInventoryWidget();
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
            if (hud.TooltipWidget)
            {
                hud.TooltipWidget.Show(args.NewClosestInteractable);

                if (args.NewClosestInteractable)
                {
                    UpdateInteractableTooltip(args.NewClosestInteractable);
                }
            }
        }

        private void OnInventorySlotInteracted(InventorySlotInteractedEventArgs args)
        {
            if (args.ItemInSlotData)
            {
                inventory.DropItem(args.ItemInSlotData, objectPools);
                UpdateInventoryWidget();
            }
        }
        
        private void OnInventoryCloseButtonInteracted()
        {
            ToggleInventoryWidget();
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

        private void ToggleInventoryWidget()
        {
            hud.PlayerInventoryWidget.ToggleShow();
            UpdateInventoryWidget();
        }
        
        private void UpdateInventoryWidget()
        {
            hud.PlayerInventoryWidget.ShowItems(inventory.GetItems());
        }
    }
}
