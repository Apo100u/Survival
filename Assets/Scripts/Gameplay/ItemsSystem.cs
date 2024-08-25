using System.Collections.Generic;
using SurvivalGame.ScriptableObjects;
using UnityEngine;

namespace SurvivalGame.Gameplay
{
    public class ItemsSystem : MonoBehaviour
    {
        [Tooltip("Assign all items that are available in the game.")]
        [SerializeField] private ItemData[] items;

        private Dictionary<int, ItemData> itemDatasByUniqueId = new();
        private int nextUniqueId;
        
        public void Init()
        {
            AssignUniqueIdsToItems();
        }

        private void AssignUniqueIdsToItems()
        {
            nextUniqueId = int.MinValue;

            for (int i = 0; i < items.Length; i++)
            {
                itemDatasByUniqueId.Add(MoveNextUniqueId(), items[i]);
            }
        }

        private int MoveNextUniqueId()
        {
            return ++nextUniqueId;
        }
    }
}
