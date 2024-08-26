using SurvivalGame.Gameplay.Entities;
using SurvivalGame.Gameplay.Items;
using UnityEngine;

namespace SurvivalGame.Gameplay
{
    public class Gameplay : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private ItemsSystem itemsSystem;
        [Tooltip("GameObject that holds all entities in the scene - used for easier searching.")]
        [SerializeField] private Transform entitiesParent;
        
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
