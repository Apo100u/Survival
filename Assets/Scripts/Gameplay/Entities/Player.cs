using UnityEngine;
using SurvivalGame.Gameplay.Entities.Components;

namespace SurvivalGame.Gameplay.Entities
{
    [RequireComponent(typeof(Movement))]
    public class Player : Entity
    {
        private Movement movement;
        
        public override void Init()
        {
            AssignComponents();
        }

        private void AssignComponents()
        {
            movement = GetComponent<Movement>();
        }
    }
}
