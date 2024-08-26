using System;
using System.Collections.Generic;
using SurvivalGame.ScriptableObjects;
using UnityEngine;

namespace SurvivalGame.UI.Widgets
{
    public class InventoryWidget : Widget
    {
        [Header("Dependencies")]
        [SerializeField] private SlotWidget[] slotsInOrder;

        public event Action<InventorySlotInteractedEventArgs> SlotInteracted;
        
        private Dictionary<SlotWidget, ItemData> itemsBySlots = new();

        public override void Init(Camera camera, RectTransform area)
        {
            base.Init(camera, area);

            for (int i = 0; i < slotsInOrder.Length; i++)
            {
                slotsInOrder[i].Init(camera, area);
                slotsInOrder[i].Interacted += OnSlotInteracted;
                itemsBySlots.Add(slotsInOrder[i], null);
            }
        }

        private void OnSlotInteracted(SlotInteractedEventArgs slotInteractedEventArgs)
        {
            SlotWidget slot = slotInteractedEventArgs.SlotWidget;
            ItemData itemData = itemsBySlots[slot];
            
            SlotInteracted?.Invoke(new InventorySlotInteractedEventArgs(slot, itemData));
        }

        public void ShowItems(IList<ItemData> items)
        {
            if (items.Count > slotsInOrder.Length)
            {
                Debug.Log("TODO: Handle case when there is more items to show than slots in InventoryWidget.");
            }
            
            for (int i = 0; i < slotsInOrder.Length; i++)
            {
                SlotWidget slot = slotsInOrder[i];
                
                if (i < items.Count)
                {
                    ItemData itemData = items[i];

                    slot.SetDisplayImage(itemData.Image);
                    itemsBySlots[slot] = itemData;
                }
                else
                {
                    slot.SetDisplayImage(null);
                    itemsBySlots[slot] = null;
                }
            }
        }
    }

    public class InventorySlotInteractedEventArgs : SlotInteractedEventArgs
    {
        public readonly ItemData ItemInSlotData;
        
        public InventorySlotInteractedEventArgs(SlotWidget slotWidget, ItemData itemInSlotData) : base(slotWidget)
        {
            ItemInSlotData = itemInSlotData;
        }
    }
}
