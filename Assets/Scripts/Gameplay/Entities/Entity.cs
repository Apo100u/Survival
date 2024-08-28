using SurvivalGame.Gameplay.Helpers;
using UnityEngine;
using UnityEngine.Serialization;

namespace SurvivalGame.Gameplay.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        [Header("Entity Dependencies")]
        [Tooltip("GameObject with visual representation of the entity (mesh, particles, etc.).")]
        [SerializeField] protected Transform visualsParent;

        protected ObjectPools objectPools;
        
        public virtual void Init(ObjectPools objectPools)
        {
            this.objectPools = objectPools;
        }
    }
}
