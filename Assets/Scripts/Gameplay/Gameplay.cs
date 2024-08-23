using SurvivalGame.Gameplay.Entities;
using UnityEngine;

namespace SurvivalGame.Gameplay
{
    public class Gameplay : MonoBehaviour
    {
        [Header("Dependencies")]
        [Tooltip("Assign GameObject that holds all entities in the scene - used for easier searching.")]
        [SerializeField] private Transform entitiesParent;
        
        private void Start()
        {
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
