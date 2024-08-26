using SurvivalGame.Gameplay.Entities;
using SurvivalGame.Gameplay.Helpers;
using SurvivalGame.Gameplay.Items;
using UnityEngine;

namespace SurvivalGame.Gameplay
{
    public class Gameplay : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private ItemsSystem itemsSystem;
        [SerializeField] private ObjectPools objectPools;
        [SerializeField] private Transform entitiesParent;
        [SerializeField] private Transform resourcesParent;
        
        private void Awake()
        {
            itemsSystem.Init();
            InitEntities();
        }

        private void InitEntities()
        {
            Entity[] entities = entitiesParent.GetComponentsInChildren<Entity>();

            for (int i = 0; i < entities.Length; i++)
            {
                entities[i].Init();
            }
        }
    }
}
