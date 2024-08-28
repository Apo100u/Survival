using SurvivalGame.Gameplay.Entities.Components;
using SurvivalGame.Gameplay.Helpers;
using UnityEngine;

namespace SurvivalGame.Gameplay.Entities
{
    [RequireComponent(typeof(Visuals))]
    public abstract class Entity : MonoBehaviour
    {
        protected Visuals visuals;
        protected ObjectPools objectPools;
        
        public virtual void Init(ObjectPools objectPools)
        {
            this.objectPools = objectPools;
            
            visuals = GetComponent<Visuals>();
            visuals.Init(objectPools);
        }
    }
}
