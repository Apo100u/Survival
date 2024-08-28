using SurvivalGame.Gameplay.Helpers;
using SurvivalGame.ScriptableObjects;
using UnityEngine;

namespace SurvivalGame.Gameplay.Entities.Components
{
    public class Visuals : MonoBehaviour
    {
        private const float DefaultPositionY = 0.0f;
        
        [Header("Visuals Dependencies")]
        [SerializeField] private Transform visualsParent;
        [SerializeField] private Transform[] hands;

        [Header("Visuals settings")]
        [SerializeField] private float walkingAnimationSpeed = 5.0f;
        [SerializeField] private float walkingAnimationSmoothness = 5.0f;
        [SerializeField] private float walkingAnimationMaxY = 0.3f;

        private bool isWalkingAnimationActive;
        private float walkingAnimationSinProgress;
        private float targetPositionY;
        private ObjectPools objectPools;
        private GameObject[] itemsInHands;
        private ItemData[] datasOfItemsInHands;
        
        public void Init(ObjectPools objectPools)
        {
            this.objectPools = objectPools;
            itemsInHands = new GameObject[hands.Length];
            datasOfItemsInHands = new ItemData[hands.Length];
        }

        public void Update()
        {
            if (isWalkingAnimationActive)
            {
                walkingAnimationSinProgress += Time.deltaTime * walkingAnimationSpeed;
                targetPositionY = Mathf.Abs(Mathf.Sin(walkingAnimationSinProgress) * walkingAnimationMaxY);
            }
            
            Vector3 targetPosition = visualsParent.transform.localPosition;
            float distanceDelta = walkingAnimationSmoothness * Time.deltaTime;
            targetPosition.y = targetPositionY;
            visualsParent.transform.localPosition = Vector3.MoveTowards(visualsParent.transform.localPosition, targetPosition, distanceDelta);
        }

        public void LookAt(Vector3 worldPosition)
        {
            visualsParent.LookAt(worldPosition);
            Vector3 rotationOnlyY = visualsParent.eulerAngles;
            rotationOnlyY.x = 0.0f;
            rotationOnlyY.z = 0.0f;

            visualsParent.eulerAngles = rotationOnlyY;
        }

        public void SetWalkingAnimationActive(bool active)
        {
            isWalkingAnimationActive = active;

            if (!isWalkingAnimationActive)
            {
                targetPositionY = DefaultPositionY;
                walkingAnimationSinProgress = 0.0f;
            }
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
