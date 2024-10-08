using System.Collections.Generic;
using System.Collections.ObjectModel;
using SurvivalGame.Gameplay.Helpers;
using SurvivalGame.ScriptableObjects;
using UnityEngine;

namespace SurvivalGame.Gameplay.Entities.Components
{
    public class Inventory : MonoBehaviour
    {
        [Header("Inventory settings")]
        [SerializeField] private int capacity = 16;
        [Tooltip("When dropping items from the inventory, they will appear in random location within this range.")]
        [SerializeField] private float dropRange = 3.0f;
        
        private List<ItemData> items = new();

        public ReadOnlyCollection<ItemData> GetItems()
        {
            return items.AsReadOnly();
        }

        public bool HasFreeSpace()
        {
            return items.Count < capacity;
        }

        public bool TryAddItem(ItemData item)
        {
            bool itemAddedSuccessfully = false;
            
            if (HasFreeSpace())
            {
                items.Add(item);
                itemAddedSuccessfully = true;
            }

            return itemAddedSuccessfully;
        }

        public void RemoveItem(ItemData item)
        {
            items.Remove(item);
        }

        public void DropItem(ItemData item, ObjectPools objectPools)
        {
            RemoveItem(item);

            GameObject itemObject = objectPools.GetFromPool(item.Prefab);
            Vector3 dropLocation = GetRandomLocationInDropRange();

            itemObject.transform.position = dropLocation;
        }

        private Vector3 GetRandomLocationInDropRange()
        {
            Vector2 randomLocationInCircle = Random.insideUnitCircle * dropRange;
            Vector3 randomLocationInDropRange = transform.position;

            randomLocationInDropRange.x += randomLocationInCircle.x;
            randomLocationInDropRange.z += randomLocationInCircle.y;

            return randomLocationInDropRange;
        }
    }
}
