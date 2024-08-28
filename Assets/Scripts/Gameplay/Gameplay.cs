using SurvivalGame.Gameplay.Entities;
using SurvivalGame.Gameplay.Entities.Player;
using SurvivalGame.Gameplay.Helpers;
using SurvivalGame.Gameplay.Items;
using UnityEngine;

namespace SurvivalGame.Gameplay
{
    public class Gameplay : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Player player;
        [SerializeField] private ItemsSystem itemsSystem;
        [SerializeField] private ObjectPools objectPools;
        [SerializeField] private Transform entitiesParent;
        
        private void Awake()
        {
            itemsSystem.Init();
            InitEntities();
        }

        private void InitEntities()
        {
            player.ItemsSystem = itemsSystem;
            
            Entity[] entities = entitiesParent.GetComponentsInChildren<Entity>();

            for (int i = 0; i < entities.Length; i++)
            {
                entities[i].Init(objectPools);
            }

        }
    }
}
