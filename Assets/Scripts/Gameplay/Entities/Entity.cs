using UnityEngine;

namespace SurvivalGame.Gameplay.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        [Header("Entity Dependencies")]
        [Tooltip("Visual representation of the entity (mesh, particles, etc.).")]
        [SerializeField] private Transform visuals;
        
        public abstract void Init();

        protected void UpdateVisualsLookAt(Vector3 lookAtWorldPosition)
        {
            visuals.LookAt(lookAtWorldPosition);
        }
    }
}
