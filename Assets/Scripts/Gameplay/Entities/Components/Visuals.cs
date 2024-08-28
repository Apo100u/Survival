using SurvivalGame.Gameplay.Helpers;
using SurvivalGame.ScriptableObjects;
using UnityEngine;

namespace SurvivalGame.Gameplay.Entities.Components
{
    public class Visuals : MonoBehaviour
    {
        [Header("Visuals Dependencies")]
        [SerializeField] private Transform visualsParent;
        [SerializeField] private Transform[] hands;

        private ObjectPools objectPools;
        private GameObject[] itemsInHands;
        private ItemData[] datasOfItemsInHands;
        
        public void Init(ObjectPools objectPools)
        {
            this.objectPools = objectPools;
            itemsInHands = new GameObject[hands.Length];
            datasOfItemsInHands = new ItemData[hands.Length];
        }

        public void LookAt(Vector3 worldPosition)
        {
            visualsParent.LookAt(worldPosition);
        }

        public void AddItemToNextFreeHand(ItemData itemData)
        {
            for (int i = 0; i < itemsInHands.Length; i++)
            {
                if (!itemsInHands[i])
                {
                    GameObject itemObject = objectPools.GetFromPool(itemData.Prefab);
                    itemsInHands[i] = itemObject;
                    datasOfItemsInHands[i] = itemData;

                    itemObject.transform.position = hands[i].position;
                    itemObject.transform.SetParent(hands[i], true);
                    
                    break;
                }
            }
        }

        public void RemoveItemFromHand(ItemData itemData)
        {
            for (int i = 0; i < datasOfItemsInHands.Length; i++)
            {
                if (datasOfItemsInHands[i] == itemData)
                {
                    itemsInHands[i].transform.SetParent(null);
                    objectPools.ReturnToPool(itemData.Prefab, itemsInHands[i]);
                    itemsInHands[i] = null;
                    datasOfItemsInHands[i] = null;

                    break;
                }
            }
        }

        public void ClearAllItemsInHands()
        {
            for (int i = 0; i < datasOfItemsInHands.Length; i++)
            {
                if (datasOfItemsInHands[i])
                {
                    RemoveItemFromHand(datasOfItemsInHands[i]);
                }
            }
        }
    }
}
