using SurvivalGame.Gameplay.Helpers;
using UnityEngine;

namespace SurvivalGame.Gameplay.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        [Header("Entity Dependencies")]
        [Tooltip("Visual representation of the entity (mesh, particles, etc.).")]
        [SerializeField] protected Transform visuals;

        protected ObjectPools objectPools;
        
        public virtual void Init(ObjectPools objectPools)
        {
            this.objectPools = objectPools;
        }
    }
}
