using System.Collections.Generic;
using SurvivalGame.ScriptableObjects;
using UnityEngine;

namespace SurvivalGame.Gameplay.Entities.Components
{
    public class Equipment : MonoBehaviour
    {
        private Dictionary<ItemData, int> itemCountPairs = new();

        public void AddItem(ItemData item)
        {
            if (!itemCountPairs.TryAdd(item, 1))
            {
                itemCountPairs[item]++;
            }
        }
    }
}
