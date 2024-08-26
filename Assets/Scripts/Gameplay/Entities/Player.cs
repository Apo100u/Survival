using System;
using System.Text;
using UnityEngine;
using SurvivalGame.Gameplay.Entities.Components;
using SurvivalGame.Gameplay.Interactions;
using SurvivalGame.Gameplay.Items;
using SurvivalGame.UI;
using Input = SurvivalGame.Gameplay.Entities.Components.Input;

namespace SurvivalGame.Gameplay.Entities
{
    [RequireComponent(typeof(Input), typeof(Movement), typeof(Equipment))]
    [RequireComponent(typeof(InteractionHandler))]
    public class Player : Entity
    {
        [Header("Player Dependencies")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private PlayerHUD hud;
        
        private Input input;
        private Movement movement;
        private Equipment equipment;
        private InteractionHandler interactionHandler;
        
        public override void Init()
        {
            AssignComponents();
            hud.Init(mainCamera);
        }

        private void AssignComponents()
        {
            input              = GetComponent<Input>();
            movement           = GetComponent<Movement>();
            equipment          = GetComponent<Equipment>();
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
        }
        
        private void OnInteractionExecuted(InteractionExecutedEventArgs args)
        {
            Item item = args.Interactable.GetComponent<Item>();

            if (item)
            {
                equipment.AddItem(item.ItemData);
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
    }
}
