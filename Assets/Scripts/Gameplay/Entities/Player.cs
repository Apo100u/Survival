using UnityEngine;
using SurvivalGame.Gameplay.Entities.Components;
using Input = SurvivalGame.Gameplay.Entities.Components.Input;

namespace SurvivalGame.Gameplay.Entities
{
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Input))]
    public class Player : Entity
    {
        private Movement movement;
        private Input input;
        
        public override void Init()
        {
            AssignComponents();
        }

        private void AssignComponents()
        {
            movement = GetComponent<Movement>();
            input = GetComponent<Input>();
        }

        private void Update()
        {
            movement.Move(input.GetMovementInput());
        }
    }
}
