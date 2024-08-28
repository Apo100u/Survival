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
            visuals = GetComponent<Visuals>();
            this.objectPools = objectPools;
        }
    }
}
