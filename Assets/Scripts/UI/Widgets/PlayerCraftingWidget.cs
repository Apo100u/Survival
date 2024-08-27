using System;
using System.Collections.Generic;
using SurvivalGame.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalGame.UI.Widgets
{
    public class PlayerCraftingWidget : InventoryWidget
    {
        [Header("Player Crafting Widget Dependencies")]
        [SerializeField] private Button backButton;
        [SerializeField] private Button craftButton;
        [SerializeField] private SlotWidget[] ingredientsSlotsInOrder;
        [SerializeField] private SlotWidget outputSlot;
        
        public event Action BackButtonInteracted;
        public event Action CraftButtonInteracted;
        
        private Dictionary<SlotWidget, ItemData> ingredientsBySlots = new();
        
        public override void Init(Camera camera, RectTransform area)
        {
            base.Init(camera, area);

            for (int i = 0; i < ingredientsSlotsInOrder.Length; i++)
            {
                ingredientsSlotsInOrder[i].Init(camera, area);
                ingredientsBySlots.Add(ingredientsSlotsInOrder[i], null);
            }

            SlotInteracted += OnSlotInteracted;
            
            backButton.onClick.AddListener(OnBackButtonInteracted);
            craftButton.onClick.AddListener(OnCraftButtonInteracted);
        }

        public override void Show(bool show)
        {
            base.Show(show);

            if (!show)
            {
                Clear();
            }
        }

        private void Clear()
        {
            for (int i = 0; i < ingredientsSlotsInOrder.Length; i++)
            {
                SlotWidget ingredientSlot = ingredientsSlotsInOrder[i];
                ingredientSlot.Clear();
                ingredientsBySlots[ingredientSlot] = null;
            }

            outputSlot.Clear();
        }

        private void OnSlotInteracted(InventorySlotInteractedEventArgs args)
        {
            if (TryShowNextIngredient(args.ItemInSlotData))
            {
                args.SlotWidget.Clear();
            }
        }

        private void OnBackButtonInteracted()
        {
            BackButtonInteracted?.Invoke();
        }

        private void OnCraftButtonInteracted()
        {
            CraftButtonInteracted?.Invoke();
        }

        private bool TryShowNextIngredient(ItemData ingredientItemData)
        {
            bool ingredientShownSuccessfully = false;
            
            for (int i = 0; i < ingredientsSlotsInOrder.Length; i++)
            {
                SlotWidget ingredientSlot = ingredientsSlotsInOrder[i];
                
                if (!ingredientsBySlots[ingredientSlot])
                {
                    ingredientSlot.SetDisplayImage(ingredientItemData.Image);
                    ingredientSlot.SetTooltip(ingredientItemData.DisplayName);
                    ingredientsBySlots[ingredientSlot] = ingredientItemData;

                    ingredientShownSuccessfully = true;
                    break;
                }
            }

            return ingredientShownSuccessfully;
        }
    }
}
