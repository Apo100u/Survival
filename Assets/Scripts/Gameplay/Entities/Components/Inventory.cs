using System.Collections.Generic;
using System.Collections.ObjectModel;
using SurvivalGame.Gameplay.Helpers;
using SurvivalGame.ScriptableObjects;
using UnityEngine;

namespace SurvivalGame.Gameplay.Entities.Components
{
    public class Inventory : MonoBehaviour
    {
        [Tooltip("When dropping items from the inventory, they will appear in random location within this range.")]
        [SerializeField] private float dropRange = 3.0f;
        
        private List<ItemData> items = new();

        public ReadOnlyCollection<ItemData> GetItems()
        {
            return items.AsReadOnly();
        }

        public void AddItem(ItemData item)
        {
            items.Add(item);
        }

        public void DropItem(ItemData item, ObjectPools objectPools)
        {
            items.Remove(item);

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
