using System.Collections.Generic;
using SurvivalGame.ScriptableObjects;
using UnityEngine;

namespace SurvivalGame.UI.Widgets
{
    public class InventoryWidget : Widget
    {
        [Header("Dependencies")]
        [SerializeField] private SlotWidget[] slotsInOrder;

        private Dictionary<SlotWidget, ItemData> itemsBySlots = new();

        public override void Init(Camera camera, RectTransform area)
        {
            base.Init(camera, area);

            for (int i = 0; i < slotsInOrder.Length; i++)
            {
                itemsBySlots.Add(slotsInOrder[i], null);
            }
        }

        public void ShowItems(IList<ItemData> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                SlotWidget slot = slotsInOrder[i];
                ItemData itemData = items[i];
                
                slot.SetDisplayImage(itemData.Image);
                itemsBySlots[slot] = itemData;

                if (i >= slotsInOrder.Length)
                {
                    Debug.Log("TODO: Handle case when inventory has more items than there are slots in inventory widget");
                }
            }
        }
    }
}
