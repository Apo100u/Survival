using System;
using System.Collections.Generic;
using SurvivalGame.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalGame.UI.Widgets
{
    public class PlayerCraftingWidget : InventoryWidget
    {
        [Header("Player Crafting Widget Dependencies")]
        [SerializeField] private Button backButton;
        [SerializeField] private Button craftButton;
        [SerializeField] private TMP_Text successChanceText;
        [SerializeField] private SlotWidget[] ingredientsSlotsInOrder;
        [SerializeField] private SlotWidget outputSlot;

        public event Action<IngredientEventArgs> IngredientAdded;
        public event Action<IngredientEventArgs> IngredientRemoved;
        public event Action BackButtonInteracted;
        public event Action CraftButtonInteracted;
        
        private Dictionary<SlotWidget, ItemData> ingredientsBySlots = new();
        
        public override void Init(Camera camera, RectTransform area)
        {
            base.Init(camera, area);

            for (int i = 0; i < ingredientsSlotsInOrder.Length; i++)
            {
                SlotWidget ingredientSlot = ingredientsSlotsInOrder[i];
                ingredientSlot.Init(camera, area);
                ingredientsBySlots.Add(ingredientSlot, null);
                ingredientSlot.Interacted += OnIngredientSlotInteracted;
            }

            InventorySlotInteracted += OnInventorySlotInteracted;
            
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

        public void UpdateOutputSlot(RecipeData recipeData)
        {
            successChanceText.gameObject.SetActive(recipeData);
            
            if (recipeData)
            {
                int percentageSuccessChance = (int)(recipeData.SuccessChance * 100.0f);
                successChanceText.text = $"Success chance: {percentageSuccessChance}%";
                outputSlot.SetDisplayImage(recipeData.SuccessfulOutput.Image);
                outputSlot.SetTooltip(recipeData.SuccessfulOutput.DisplayName);
            }
            else
            {
                outputSlot.Clear();
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

        private void OnInventorySlotInteracted(InventorySlotInteractedEventArgs args)
        {
            if (args.ItemInSlotData && TryShowNextIngredient(args.ItemInSlotData))
            {
                ShowItemInInventorySlot(args.SlotWidget, null);
            }
        }
        
        private void OnIngredientSlotInteracted(SlotInteractedEventArgs args)
        {
            if (ingredientsBySlots[args.SlotWidget])
            {
                RemoveIngredient(args.SlotWidget);
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
                    AddIngredient(ingredientSlot, ingredientItemData);
                    ingredientShownSuccessfully = true;
                    
                    break;
                }
            }

            return ingredientShownSuccessfully;
        }

        private void AddIngredient(SlotWidget ingredientSlot, ItemData ingredientItemData)
        {
            ingredientSlot.SetDisplayImage(ingredientItemData.Image);
            ingredientSlot.SetTooltip(ingredientItemData.DisplayName);
            ingredientsBySlots[ingredientSlot] = ingredientItemData;

            IngredientAdded?.Invoke(new IngredientEventArgs(ingredientItemData));
        }

        private void RemoveIngredient(SlotWidget ingredientSlot)
        {
            IngredientRemoved?.Invoke(new IngredientEventArgs(ingredientsBySlots[ingredientSlot]));
                
            ShowItemInFirstEmptyInventorySlot(ingredientsBySlots[ingredientSlot]);
            ingredientSlot.Clear();
            ingredientsBySlots[ingredientSlot] = null;
        }
    }

    public class IngredientEventArgs
    {
        public readonly ItemData Ingredient;

        public IngredientEventArgs(ItemData ingredient)
        {
            Ingredient = ingredient;
        }
    }
}
