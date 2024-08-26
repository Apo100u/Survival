using System.Collections.Generic;
using System.Collections.ObjectModel;
using SurvivalGame.ScriptableObjects;
using UnityEngine;

namespace SurvivalGame.Gameplay.Entities.Components
{
    public class Inventory : MonoBehaviour
    {
        private List<ItemData> items = new();

        public void AddItem(ItemData item)
        {
            items.Add(item);
        }

        public ReadOnlyCollection<ItemData> GetItems()
        {
            return items.AsReadOnly();
        }
    }
}
